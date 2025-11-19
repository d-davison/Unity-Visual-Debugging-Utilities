using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugWorldItem : MonoBehaviour
    {
        private DebugDrawController _controller;
        private Shapes _shape;
        private float _timeLeft;
        
        public virtual void ConfigureItem(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, 
            float duration = 5f, Color color = default, float size = 1f)
        {
            _controller = controller;
            _shape = shape;
            
            transform.position = position;
            transform.eulerAngles = rotation;
            
            _timeLeft = duration;
            
            transform.localScale = new Vector3(size, size, size);
        }

        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _controller.Release(this, _shape);
                //This will turn the gameObject off and stop update
            }
        }
    }
}
