using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    [RequireComponent(typeof(DebugScreenController))]
    public class DebugScreenTester : MonoBehaviour
    {
        [SerializeField] private string testingString = "Testing";
        [SerializeField] private float testingLength = 5;
        [SerializeField] private Color testingColor = Color.white;
        
        private DebugScreenController _debugScreenController;

        private void Start()
        {
            _debugScreenController = GetComponent<DebugScreenController>();
        }

        public void SendDebugMessage()
        {
            _debugScreenController.AddNewText(testingString, testingLength, testingColor);
        }
    }
}
