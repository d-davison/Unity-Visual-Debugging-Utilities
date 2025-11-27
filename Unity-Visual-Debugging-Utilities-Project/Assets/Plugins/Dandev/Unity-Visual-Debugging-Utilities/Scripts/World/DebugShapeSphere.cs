using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShapeSphere : DebugShape
    {
        [SerializeField] private int segments = 16;

        public override void DrawShape()
        {
            base.DrawShape();
   
            #if UNITY_EDITOR
            RemoveAllLineRenderers();
            
            Material targetMaterial = Resources.Load<Material>(ResourcesPath_Material);
            
            // We need 3 LineRenderers for 3 circles (XY, XZ, YZ)
            for (int i = 0; i < 3; i++)
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
            
            UpdateSphereVertices();
            
            #else
            Debug.LogWarning("Editor only function.");
            #endif
        }
        
        private void UpdateSphereVertices()
        {
            if (lineRenderers.Count < 3) return;

            float radius = size / 2;

            // Circle on XY plane (Z is 0)
            DrawCircle(lineRenderers[0], radius, 0);
            
            // Circle on XZ plane (Y is 0)
            DrawCircle(lineRenderers[1], radius, 1);
            
            // Circle on YZ plane (X is 0)
            DrawCircle(lineRenderers[2], radius, 2);
        }
        
        private void DrawCircle(LineRenderer lr, float radius, int axisIndex)
        {
            // axisIndex: 0 = XY plane, 1 = XZ plane, 2 = YZ plane
            
            for (int i = 0; i < segments; i++)
            {
                float angle = i * (2f * Mathf.PI / segments);
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                Vector3 point = Vector3.zero;
                switch (axisIndex)
                {
                    case 0: // XY
                        point = new Vector3(x, y, 0);
                        break;
                    case 1: // XZ
                        point = new Vector3(x, 0, y);
                        break;
                    case 2: // YZ
                        point = new Vector3(0, x, y);
                        break;
                }
                
                lr.SetPosition(i, point);
            }
        }
    }
}
