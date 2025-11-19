using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugDrawController : MonoBehaviour
    {
        private IObjectPool<DebugShapeCube> _cubes;
        private IObjectPool<DebugShapeSphere> _spheres;
        private IObjectPool<DebugText> _texts;
        
        private const string ResourcesPath_Cube = "DebugShapeCube";
        private const string ResourcesPath_Sphere = "DebugShapeSphere";
        private const string ResourcesPath_Text = "DebugText";

        public void DrawItem(Shapes shape, Vector3 position, Vector3 rotation, Color color, float duration, float size, string text = null)
        {
            switch (shape)
            {
                case Shapes.Sphere:
                    DebugShapeSphere sphere = _spheres.Get();
                    sphere.transform.position = position;
                    sphere.ConfigureItem(this, shape, position, rotation, duration, color, size);
                    break;
                case Shapes.Cube:
                    DebugShapeCube cube = _cubes.Get();
                    break;
                case Shapes.Text:
                    DebugText debugText = _texts.Get();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
            }
        }
        
        public void Release(DebugWorldItem debugWorldItem, Shapes shape)
        {
            switch (shape)
            {
                case Shapes.Sphere:
                    _spheres.Release(debugWorldItem as DebugShapeSphere);
                    break;
                case Shapes.Cube:
                    _cubes.Release(debugWorldItem as DebugShapeCube);
                    break;
                case Shapes.Text:
                    _texts.Release(debugWorldItem as DebugText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
            }
        }


        private void Awake()
        {
            _cubes = CreateObjectPool<DebugShapeCube>(ResourcesPath_Cube, 5);
            _spheres = CreateObjectPool<DebugShapeSphere>(ResourcesPath_Sphere, 5);
            _texts = CreateObjectPool<DebugText>(ResourcesPath_Text, 10);
        }
        
        private IObjectPool<T> CreateObjectPool<T>(string resourcePath, int capacity) where T : Component
        {
            return new ObjectPool<T>(
                createFunc: () =>
                {
                    T prefab = Resources.Load<T>(resourcePath);
                    if (prefab == null)
                    {
                        Debug.LogError($"Could not find resource at path: {resourcePath}");
                        return null;
                    }

                    return Instantiate(prefab, transform);
                },
                actionOnGet: item => { item.gameObject.SetActive(true); },
                actionOnRelease: item => { item.gameObject.SetActive(false); },
                actionOnDestroy: item => Destroy(item.gameObject),
                defaultCapacity: capacity
            );
        }
    }
}
