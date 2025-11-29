using System;
using Plugins.Dandev.Unity_Visual_Debugging_Utilities.Scripts;
using UnityEngine;
using UnityEngine.Pool;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    public class DebugDrawController : MonoBehaviour
    {
        private IObjectPool<DebugShapeCube> _cubes;
        private IObjectPool<DebugShapeSphere> _spheres;
        private IObjectPool<DebugText> _texts;

        public void DrawItem(Shapes shape, Vector3 position, Vector3 rotation, Color color, float duration, float size, string label = null)
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
                    cube.transform.position = position;
                    cube.ConfigureItem(this, shape, position, rotation, duration, color, size);
                    break;
                case Shapes.Text:
                    DebugText text = _texts.Get();
                    text.transform.position = position;
                    text.ConfigureText(this, shape, position, rotation, label, duration, color, size);
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
            _cubes = CreateObjectPool<DebugShapeCube>(DebugUtilities.CubePath, 5);
            _spheres = CreateObjectPool<DebugShapeSphere>(DebugUtilities.SpherePath, 5);
            _texts = CreateObjectPool<DebugText>(DebugUtilities.TextPath, 10);
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
