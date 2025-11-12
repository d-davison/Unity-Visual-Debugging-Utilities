using UnityEditor;
using UnityEngine;

namespace Dandev.Unity_Visual_Debugging_Utilities
{
    [CustomEditor(typeof(DebugScreenTester))]
    public class DebugScreenTester_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DebugScreenTester tester = (DebugScreenTester)target;
            if (GUILayout.Button("Send Debug Message"))
            {
                tester.SendDebugMessage();
            }
        }
    }
}