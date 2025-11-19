using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShapeDrawer : MonoBehaviour
    {
        public void DrawShape()
        {
            DebugShape shape = GetComponent<DebugShape>();
            if (shape)
            {
                shape.DrawShape();
            }
            else
            {
                Debug.LogError("No DebugShape component found on this GameObject.");
            }
        }
    }
}
