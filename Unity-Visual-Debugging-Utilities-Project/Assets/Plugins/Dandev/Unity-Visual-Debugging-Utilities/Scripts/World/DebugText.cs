using TMPro;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugText : DebugWorldItem
    {
        [SerializeField] private TextMeshProUGUI label;
        
        public void ConfigureText(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, string text, float duration = 5,
            Color color = default, float size = 1)
        {
            ConfigureItem(controller, shape, position, rotation, duration, color, size);
            
            label.text = text;
        }
    }
}
