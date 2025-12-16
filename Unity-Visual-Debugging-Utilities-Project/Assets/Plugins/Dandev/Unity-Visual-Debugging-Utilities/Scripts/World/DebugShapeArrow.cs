using Plugins.Dandev.Unity_Visual_Debugging_Utilities.Scripts;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugShapeArrow : DebugShape
    {
        [Tooltip("The width of the shaft of the arrow in relation to the length of the arrow.")]
        [Range(0.1f, 0.5f)][SerializeField] private float shaftWidthScale = 0.5f;
        
        [Tooltip("The width of the arrow head in relation to the length of the arrow.")]
        [Range(0.2f, 2.0f)][SerializeField] private float arrowHeadWidthScale = 1.0f;
        
        [Tooltip("The length of the arrow head in relation to the length of the arrow.")]
        [Range(0.1f, 0.9f)][SerializeField] private float arrowHeadLengthScale = 0.2f;
        
        [Tooltip("The width of the arrow head in relation to the length of the arrow.")]
        [Range(0.1f, 0.9f)][SerializeField] private float arrowDepthScale = 0.2f;

        private readonly int _segmentsRequired = 9;
        
        public override void DrawShape()
        {
            base.DrawShape();
   
#if UNITY_EDITOR
            RemoveAllLineRenderers();
            
            Material targetMaterial = Resources.Load<Material>(DebugUtilities.AlwaysOnTopMaterialPath);
            
            for (int i = 0; i < _segmentsRequired; i++)
            {
                var child = new GameObject($"Circle_{i}");
                child.transform.SetParent(transform, false);
                        
                var lr = child.AddComponent<LineRenderer>();
                lr.useWorldSpace = false;
                lr.widthMultiplier = width;
                lr.loop = true;
                lr.material = targetMaterial;
                lineRenderers.Add(lr);
            }

            DrawArrow();

#else
            Debug.LogWarning("Editor only function.");
#endif
        }

         private void DrawArrow()
        {
            if (lineRenderers == null || lineRenderers.Count < _segmentsRequired)
                return;

            // Arrow is drawn in local space:
            // - Forward along +Z
            // - Width along X
            // - "Stacking distance" along Y (uses `width` as requested)
            float length = Mathf.Max(0.0001f, size);
            float headLength = Mathf.Clamp01(arrowHeadLengthScale) * length;
            float shaftLength = Mathf.Max(0.0001f, length - headLength);

            float headHalfWidth = Mathf.Max(0.0001f, length * arrowHeadWidthScale * 0.5f);
            float shaftHalfWidth = Mathf.Max(0.0001f, length * shaftWidthScale * 0.5f);

            float depth = Mathf.Max(0.0001f, size * arrowDepthScale); // distance between the two arrows
            float yFront = -depth * 0.5f;
            float yBack = depth * 0.5f;

            // Center the arrow around the origin (so "root" is in the middle of its length)
            float zCenterOffset = -length * 0.5f;

            // 7-vertex "squared" arrow profile (pointy tip, squared shoulders/shaft)
            // Loop closes automatically via LineRenderer.loop = true
            Vector3[] profile = new Vector3[]
            {
                new Vector3(-shaftHalfWidth, 0f, 0f),           // 0: tail left
                new Vector3( shaftHalfWidth, 0f, 0f),           // 1: tail right
                new Vector3( shaftHalfWidth, 0f, shaftLength),  // 2: shaft right (at head start)
                new Vector3( headHalfWidth,  0f, shaftLength),  // 3: head right (shoulder)
                new Vector3( 0f,            0f, length),        // 4: tip
                new Vector3(-headHalfWidth,  0f, shaftLength),  // 5: head left (shoulder)
                new Vector3(-shaftHalfWidth, 0f, shaftLength),  // 6: shaft left (at head start)
            };

            // Shift along Z so the arrow is centered at local origin
            for (int i = 0; i < profile.Length; i++)
            {
                profile[i] = new Vector3(profile[i].x, profile[i].y, profile[i].z + zCenterOffset);
            }

            // Build the two stacked arrows (front/back)
            var front = new Vector3[profile.Length];
            var back = new Vector3[profile.Length];
            for (int i = 0; i < profile.Length; i++)
            {
                front[i] = new Vector3(profile[i].x, yFront, profile[i].z);
                back[i] = new Vector3(profile[i].x, yBack, profile[i].z);
            }

            // 0: front outline
            var lrFront = lineRenderers[0];
            lrFront.loop = true;
            lrFront.positionCount = front.Length;
            lrFront.SetPositions(front);

            // 1: back outline
            var lrBack = lineRenderers[1];
            lrBack.loop = true;
            lrBack.positionCount = back.Length;
            lrBack.SetPositions(back);

            // 2..8: connect equivalent vertices (0..6) between front/back
            for (int i = 0; i < profile.Length; i++)
            {
                var lrBridge = lineRenderers[2 + i];
                lrBridge.loop = false;
                lrBridge.positionCount = 2;
                lrBridge.SetPosition(0, front[i]);
                lrBridge.SetPosition(1, back[i]);
            }
        }
    }
}
