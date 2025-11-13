using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugScreen : MonoBehaviour
    {
        private static DebugScreen _instance;

        public static DebugScreen Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject debugScreenObject = new GameObject("DebugScreen");
                    _instance = debugScreenObject.AddComponent<DebugScreen>();
                    _instance.Initialize();

                    DontDestroyOnLoad(debugScreenObject);
                }

                return _instance;
            }
        }

        private DebugScreenController _controller;
        private const string ResourcesPath = "DebugScreenController";

        private void Initialize()
        {
            DebugScreenController controllerPrefab = Resources.Load<DebugScreenController>(ResourcesPath);
            _controller = Instantiate(controllerPrefab, transform);
        }

        public static void Log(string label, float time, Color color)
        {
            Instance._controller.AddNewText(label, time, color);
        }
        
    }
}
