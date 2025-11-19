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
        
        public static void Sphere()
        {
            Instance._controller.DrawItem(Shapes.Sphere, Vector3.zero, Vector3.zero, Color.white, 5, 1);
        }
        
        public static void Cube()
        {
            Instance._controller.DrawItem(Shapes.Cube, Vector3.zero, Vector3.zero, Color.white, 5, 1);
        }

        public static void Text()
        {
            Instance._controller.DrawItem(Shapes.Text, Vector3.zero, Vector3.zero, Color.white, 5, 1, "Testing");
        }
    }

    public enum Shapes
    {
        Sphere,
        Cube,
        Text
    }
}
