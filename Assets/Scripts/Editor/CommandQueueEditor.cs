using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CommandQueue))]
public class CommandQueueEditor : Editor
{
    private SerializedProperty m_capacity;
    private SerializedProperty m_commandQueue;

    private void OnEnable()
    {
        m_capacity = serializedObject.FindProperty("m_capacity");
        m_commandQueue = serializedObject.FindProperty("m_commandQueue");
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(Application.isPlaying);
        EditorGUILayout.PropertyField(m_capacity);
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(true);
        for (int i = 0; i < m_commandQueue.arraySize; i++)
        {
            SerializedProperty element = m_commandQueue.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(element);
        }
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}