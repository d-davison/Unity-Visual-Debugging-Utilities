using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    [RequireComponent(typeof(Canvas))]
    public class DebugScreenController : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup layoutGroup;

        private IObjectPool<DebugScreenText> _debugScreenTextPool;
        private const string ResourcesPath = "DebugScreenText (TMP)";

        private void Awake()
        {
            _debugScreenTextPool = new ObjectPool<DebugScreenText>(
                createFunc: () =>
                {
                    DebugScreenText prefab = Resources.Load<DebugScreenText>(ResourcesPath);
                    if (prefab == null)
                    {
                        Debug.LogError($"Could not find DebugScreenText resource at path: {ResourcesPath}");
                        return null;
                    }

                    return Instantiate(prefab, layoutGroup.transform);
                },
                actionOnGet: text => { text.gameObject.SetActive(true); },
                actionOnRelease: text => { text.gameObject.SetActive(false); },
                actionOnDestroy: text => Destroy(text.gameObject),
                defaultCapacity: 10
            );
        }

        public void AddNewText(string label, float time, Color color)
        {
            DebugScreenText newText = _debugScreenTextPool.Get();
            newText.transform.SetAsFirstSibling();
            newText.ConfigureText(this, label, time, color);
        }

        public void ReleaseText(DebugScreenText text)
        {
            _debugScreenTextPool.Release(text);
        }
    }
}
