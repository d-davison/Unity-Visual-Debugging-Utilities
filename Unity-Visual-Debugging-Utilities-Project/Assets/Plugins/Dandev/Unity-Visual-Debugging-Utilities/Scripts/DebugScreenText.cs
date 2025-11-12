using TMPro;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DebugScreenText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private DebugScreenController _controller;
        private float _timeLeft;

        public void ConfigureText(DebugScreenController controller, string label, float time, Color color)
        {
            _controller = controller;
            _timeLeft = time;
            
            text.text = label;
            text.color = color;
        }

        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _controller.ReleaseText(this);
                //This will turn the gameObject off and stop update
            }
        }
    }
}