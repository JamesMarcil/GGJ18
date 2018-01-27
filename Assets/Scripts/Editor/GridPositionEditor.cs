using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPosition))]
public class GridPositionEditor : Editor
{
    private SerializedProperty m_row;
    private SerializedProperty m_column;

    private void OnEnable()
    {
        m_row = serializedObject.FindProperty("m_row");
        m_column = serializedObject.FindProperty("m_column");
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(m_row);
        EditorGUILayout.PropertyField(m_column);
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}