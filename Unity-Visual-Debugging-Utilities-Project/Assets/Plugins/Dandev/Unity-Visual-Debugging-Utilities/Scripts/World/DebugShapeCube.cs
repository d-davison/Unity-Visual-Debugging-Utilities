using Plugins.Dandev.Unity_Visual_Debugging_Utilities.Scripts;
using UnityEditor;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShapeCube : DebugShape
    {
        public override void ConfigureItem(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, float duration = 5,
            Color color = default, float size = 1)
        {
            base.ConfigureItem(controller, shape, position, rotation, duration, color, size);
        }

        public override void DrawShape()
        {
            base.DrawShape();
            
#if UNITY_EDITOR
            RemoveAllLineRenderers();
            
            Material targetMaterial = Resources.Load<Material>(DebugUtilities.AlwaysOnTopMaterialPath);
            
            var newChild = new GameObject("Cube (Line Renderer)");
            newChild.transform.parent = transform;
            newChild.transform.localPosition = Vector3.zero;
            newChild.transform.localRotation = Quaternion.identity;
                
                
            var lr = newChild.AddComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.startWidth = width;
            lr.endWidth = width;
            lr.material = targetMaterial;
            lineRenderers.Add(lr);
            
            UpdateCubeVertices();
            
            EditorUtility.SetDirty(gameObject);
#else
            Debug.LogWarning("Editor only function.");
#endif
        }
        
        private void UpdateCubeVertices()
        {
            if (lineRenderers.Count != 1) return;
            
            var lr = lineRenderers[0];
            float h = size / 2;

            // Define the 8 corners of the cube
            Vector3 p0 = new Vector3(-h, -h, -h);
            Vector3 p1 = new Vector3( h, -h, -h);
            Vector3 p2 = new Vector3( h, -h,  h);
            Vector3 p3 = new Vector3(-h, -h,  h);
            Vector3 p4 = new Vector3(-h,  h, -h);
            Vector3 p5 = new Vector3( h,  h, -h);
            Vector3 p6 = new Vector3( h,  h,  h);
            Vector3 p7 = new Vector3(-h,  h,  h);

            // A continuous path that covers all edges of the cube
            Vector3[] path = new Vector3[]
            {
                p0, p1, p2, p3, p0, // Bottom Square
                p4, // Vertical up
                p5, p1, p5, // Top front edge, vertical down, back up
                p6, p2, p6, // Top right edge, vertical down, back up
                p7, p3, p7, // Top back edge, vertical down, back up
                p4 // Top left edge to close the loop
            };

            lr.positionCount = path.Length;
            lr.SetPositions(path);
        }
    }
}
