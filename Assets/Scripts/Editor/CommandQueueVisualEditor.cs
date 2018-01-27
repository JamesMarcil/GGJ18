using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CommandQueueVisual))]
public class CommandQueueVisualEditor : Editor
{
    private SerializedProperty m_keys;
    private SerializedProperty m_values;

    private void OnEnable()
    {
        m_keys = serializedObject.FindProperty("m_keys");
        m_values = serializedObject.FindProperty("m_values");
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

        int count = Mathf.Min(m_keys.arraySize, m_values.arraySize);
        for (int i = 0; i < count; i++)
        {
            SerializedProperty key = m_keys.GetArrayElementAtIndex(i);
            SerializedProperty value = m_values.GetArrayElementAtIndex(i);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(key, new GUIContent("Type: "));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(value, new GUIContent("Prefab: "));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
