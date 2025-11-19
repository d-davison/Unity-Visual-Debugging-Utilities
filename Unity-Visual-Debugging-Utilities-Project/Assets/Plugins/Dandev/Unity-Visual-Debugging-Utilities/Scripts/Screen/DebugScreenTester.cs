using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugScreenTester : MonoBehaviour
    {
        [SerializeField] private string testingString = "Testing";
        [SerializeField] private float testingLength = 5;
        [SerializeField] private Color testingColor = Color.white;

        public void SendDebugMessage()
        {
            DebugScreen.Log(testingString, testingLength, testingColor);
        }
    }
}
