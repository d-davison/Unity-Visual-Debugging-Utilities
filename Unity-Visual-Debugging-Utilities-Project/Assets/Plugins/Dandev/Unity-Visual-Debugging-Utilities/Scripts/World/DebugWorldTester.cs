using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugWorldTester : MonoBehaviour
    {
        public void DebugDrawCube()
        {
            DebugDraw.Cube(RandomPosition(), RandomDuration(), RandomSize(), RandomColor());
        }

        public void DebugDrawSphere()
        {
            DebugDraw.Sphere(RandomPosition(), RandomDuration(), RandomSize(), RandomColor());
        }

        public void DebugDrawText()
        {
            string text = "Hello World!";
            DebugDraw.Text(RandomPosition(), text, RandomDuration(), RandomSize(), RandomColor());
        }
        
        private Vector3 RandomPosition()
        {
            return new Vector3(Random.Range(-10f, 10f), Random.Range(0, 10f), Random.Range(-10f, 10f));
        }

        private float RandomDuration()
        {
            return Random.Range(1f, 10f);
        }

        private float RandomSize()
        {
            return Random.Range(0.5f, 3f);
        }

        private Color RandomColor()
        {
            return Color.HSVToRGB(Random.value, 1f, 1f);
        }
    }
}
