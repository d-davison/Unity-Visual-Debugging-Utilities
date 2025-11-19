using UnityEngine;
using UnityEditor;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    [CustomEditor(typeof(DebugShapeDrawer))]
    public class DebugShapeDrawer_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DebugShapeDrawer tester = (DebugShapeDrawer)target;
            if (GUILayout.Button("Draw Shape"))
            {
                tester.DrawShape();
            }
        }
    }
}
