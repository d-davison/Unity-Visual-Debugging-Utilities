using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugWorldTester : MonoBehaviour
    {
        public void DebugDrawCube()
        {
            DebugDraw.Cube();
        }

        public void DebugDrawSphere()
        {
            DebugDraw.Sphere();
        }

        public void DebugDrawText()
        {
            DebugDraw.Text();
        }
    }
}
