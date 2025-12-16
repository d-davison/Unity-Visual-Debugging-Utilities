using UnityEditor;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    [CustomEditor(typeof(DebugWorldTester))]
    public class DebugWorldTester_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DebugWorldTester tester = (DebugWorldTester)target;
            if (GUILayout.Button("Draw Cube"))
            {
                tester.DebugDrawCube();
            }

            if (GUILayout.Button("Draw Sphere"))
            {
                tester.DebugDrawSphere();
            }

            if (GUILayout.Button("Draw Arrow"))
            {
                tester.DebugDrawArrow();
            }

            if (GUILayout.Button("Draw Text"))
            {
                tester.DebugDrawText();
            }
        }
    }
}