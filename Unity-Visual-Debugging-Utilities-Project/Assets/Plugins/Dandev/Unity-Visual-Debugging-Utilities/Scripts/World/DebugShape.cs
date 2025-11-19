using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShape : DebugWorldItem
    {
        [SerializeField] private List<LineRenderer> lineRenderers = new List<LineRenderer>();
        
        public override void ConfigureItem(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, float duration = 5,
            Color color = default, float size = 1)
        {
            base.ConfigureItem(controller, shape, position, rotation, duration, color, size);

            foreach (var lineRenderer in lineRenderers)
            {
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
            }
        }
    }
}
