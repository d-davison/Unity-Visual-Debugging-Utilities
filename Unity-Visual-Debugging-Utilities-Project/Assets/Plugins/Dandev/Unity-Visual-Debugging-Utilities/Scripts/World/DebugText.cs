using TMPro;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugText : DebugWorldItem
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI label;
        
        private Camera _camera;
        
        public void ConfigureText(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, string text, float duration = 5,
            Color color = default, float size = 1)
        {
            ConfigureItem(controller, shape, position, rotation, duration, color, size);
            
            _camera = Camera.main;
            label.text = text;
            if (_camera == null) return;
            canvas.worldCamera = _camera;
            transform.LookAt(_camera.transform);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
