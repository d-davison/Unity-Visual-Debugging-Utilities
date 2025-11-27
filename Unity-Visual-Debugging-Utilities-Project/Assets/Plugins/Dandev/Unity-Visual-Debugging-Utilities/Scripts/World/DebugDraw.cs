using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugDraw : MonoBehaviour
    {
        private static DebugDraw _instance;

        public static DebugDraw Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject debugScreenObject = new GameObject("DebugScreen");
                    _instance = debugScreenObject.AddComponent<DebugDraw>();
                    _instance.Initialize();

                    DontDestroyOnLoad(debugScreenObject);
                }

                return _instance;
            }
        }
        
        private DebugDrawController _controller;
        private const string ResourcesPath = "DebugDrawController";
        
        private void Initialize()
        {
            DebugDrawController controllerPrefab = Resources.Load<DebugDrawController>(ResourcesPath);
            _controller = Instantiate(controllerPrefab, transform);
        }
        
        public static void Sphere(Vector3 position, float duration = 5f, float size = 1, Color color = default)
        {
            Instance._controller.DrawItem(Shapes.Sphere, position, Vector3.zero, color, duration, size);
        }
        
        public static void Cube(Vector3 position, float duration = 5f, float size = 1, Color color = default)
        {
            Instance._controller.DrawItem(Shapes.Cube, position, Vector3.zero, color, duration, size);
        }
        
        public static void Cube(Vector3 position, Vector3 rotation, float duration = 5f, float size = 1, Color color = default)
        {
            Instance._controller.DrawItem(Shapes.Cube, position, rotation, color, duration, size);
        }

        public static void Text(Vector3 position, string label, float duration = 5f, float size = 1, Color color = default)
        {
            Instance._controller.DrawItem(Shapes.Text, position, Vector3.zero, color, duration, size, label);
        }
    }

    public enum Shapes
    {
        Sphere,
        Cube,
        Text
    }
}
