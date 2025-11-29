using System.Collections.Generic;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShape : DebugWorldItem
    {
        [SerializeField] protected float size = 1;
        [SerializeField] protected float width = 0.05f;
        [SerializeField] protected List<LineRenderer> lineRenderers = new List<LineRenderer>();
        
        protected bool _initialized = false;
        
        public override void ConfigureItem(DebugDrawController controller, Shapes shape, Vector3 position, Vector3 rotation, float duration = 5,
            Color color = default, float size = 1)
        {
            base.ConfigureItem(controller, shape, position, rotation, duration, color, size);

            foreach (var lineRenderer in lineRenderers)
            {
                lineRenderer.material.color = color;
            }
        }

        public virtual void DrawShape()
        {
            
        }

        protected void RemoveAllLineRenderers()
        {
            //Remove everything under the shape
            foreach (var trans in GetComponentsInChildren<Transform>(true))
            {
                if (trans.gameObject == this.gameObject)
                    continue;
                DestroyImmediate(trans.gameObject);
            }

            //Just in case there's some remaining somewhere else
            foreach (var lineRenderer in lineRenderers)
            {
                DestroyImmediate(lineRenderer);
            }
            
            lineRenderers.Clear();
        }
        
        protected void RemoveInvalidLineRenderers()
        {
            var invalidLineRenderers = new List<LineRenderer>();
            
            foreach (var lineRenderer in lineRenderers)
            {
                if (lineRenderer == null) invalidLineRenderers.Add(lineRenderer);
            }
            
            foreach (var lineRenderer in invalidLineRenderers)
            {
                lineRenderers.Remove(lineRenderer);
            }   
        }
    }
}
