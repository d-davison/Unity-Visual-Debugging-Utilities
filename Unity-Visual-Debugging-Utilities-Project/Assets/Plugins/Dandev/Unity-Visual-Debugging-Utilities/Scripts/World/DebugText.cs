using TMPro;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugText : DebugWorldItem
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI label;
        
        private Camera _camera;
        private float _size = 1;
        
        public void ConfigureText(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, string text, float duration = 5,
            Color color = default, float size = 1)
        {
            ConfigureItem(controller, shape, position, rotation, duration, color, size);
            
            _camera = Camera.main;
            label.text = text;
            if (_camera == null) return;
            canvas.worldCamera = _camera;
            transform.LookAt(_camera.transform);
            _size = size;
        }

        protected override void Update()
        {
            base.Update();

            if (_camera == null)
            {
                _camera = Camera.main;
            }
            
            CalculateDynamicScale();
        }

        /// <summary>
        /// Dynamically scale the text based on distance from camera.
        /// </summary>
        private void CalculateDynamicScale()
        {
            float targetScale = _size * 0.1f;
            float distance = Vector3.Distance(transform.position, _camera.transform.position);
            float multiplier = distance * 0.3f;
            float scale = targetScale * multiplier;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
