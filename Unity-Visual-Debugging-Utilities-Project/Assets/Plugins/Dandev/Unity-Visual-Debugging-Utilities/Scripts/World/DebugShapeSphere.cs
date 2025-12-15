using Plugins.Dandev.Unity_Visual_Debugging_Utilities.Scripts;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShapeSphere : DebugShape
    {
        [Tooltip("Segments are the fidelity of the sphere.")]
        [Range(6, 32)][SerializeField] private int segments = 16;
        
        [Tooltip("Horizontal rings are the circles from bottom to top (latitude). Best to be an odd number")]
        [Range(1, 32)][SerializeField] private int horizontalRings = 7;
        
        [Tooltip("Vertical rings are the circles rotated on the y axis (longitude). Best to be an even number")]
        [Range(2, 32)][SerializeField] private int verticalRings = 12;

        public override void DrawShape()
        {
            base.DrawShape();
   
            #if UNITY_EDITOR
            RemoveAllLineRenderers();
            
            Material targetMaterial = Resources.Load<Material>(DebugUtilities.AlwaysOnTopMaterialPath);
            
            int seg = Mathf.Max(6, segments);
            int latCount = Mathf.Max(1, horizontalRings);
            int lonCount = Mathf.Max(2, verticalRings);
            
            for (int i = 0; i < latCount + lonCount; i++)
            {
                var child = new GameObject($"Circle_{i}");
                child.transform.SetParent(transform, false);
                        
                var lr = child.AddComponent<LineRenderer>();
                lr.useWorldSpace = false;
                lr.widthMultiplier = width;
                lr.loop = true;
                lr.positionCount = segments;
                lr.material = targetMaterial;
                lineRenderers.Add(lr);
            }
            
            UpdateSphereVertices(seg, latCount, lonCount);
            
            #else
            Debug.LogWarning("Editor only function.");
            #endif
        }
        
        private void UpdateSphereVertices(int seg, int latCount, int lonCount)
        {
            float radius = size * 0.5f;

            int index = 0;
            
            for (int i = 0; i < latCount; i++)
            {
                float t = (i + 1f) / (latCount + 1f);          // (0..1) excluding ends
                float y = Mathf.Lerp(-radius, radius, t);      // bottom to top
                float ringRadius = Mathf.Sqrt(Mathf.Max(0f, radius * radius - y * y));

                DrawLatitudeRing(lineRenderers[index], ringRadius, y, seg);
                index++;
            }

            // Longitude rings (vertical): great circles going through the poles.
            // Construct a circle in the YZ plane, then rotate it around Y by phi.
            for (int j = 0; j < lonCount; j++)
            {
                float phi = (j / (float)lonCount) * (2f * Mathf.PI);
                DrawLongitudeRing(lineRenderers[index], radius, phi, seg);
                index++;
            }
        }
        
        private static void DrawLatitudeRing(LineRenderer lr, float ringRadius, float y, int seg)
        {
            for (int k = 0; k < seg; k++)
            {
                float a = (k / (float)seg) * (2f * Mathf.PI);
                float x = Mathf.Cos(a) * ringRadius;
                float z = Mathf.Sin(a) * ringRadius;

                lr.SetPosition(k, new Vector3(x, y, z));
            }
        }

        private static void DrawLongitudeRing(LineRenderer lr, float radius, float phi, int seg)
        {
            float cosPhi = Mathf.Cos(phi);
            float sinPhi = Mathf.Sin(phi);

            for (int k = 0; k < seg; k++)
            {
                float a = (k / (float)seg) * (2f * Mathf.PI);

                // Base great circle in YZ plane: (0, sin(a)*r, cos(a)*r)
                float y = Mathf.Sin(a) * radius;
                float z0 = Mathf.Cos(a) * radius;

                // Rotate around Y by phi: (x,z) = rotY(phi) * (0,z0)
                float x = sinPhi * z0;
                float z = cosPhi * z0;

                lr.SetPosition(k, new Vector3(x, y, z));
            }
        }
    }
}
