using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGrid))]
public class GameGridEditor : Editor
{
    private SerializedProperty m_width;
    private SerializedProperty m_height;
    private SerializedProperty m_nodeWidth;
    private SerializedProperty m_nodeHeight;

    private GameGrid m_grid;

    private void OnEnable()
    {
        m_width = serializedObject.FindProperty("m_width");
        m_height = serializedObject.FindProperty("m_height");
        m_nodeWidth = serializedObject.FindProperty("m_nodeWidth");
        m_nodeHeight = serializedObject.FindProperty("m_nodeHeight");

        m_grid = target as GameGrid;
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

        if (m_width.intValue < 0)
        {
            m_width.intValue = 0;
        }

        if (m_height.intValue < 0)
        {
            m_height.intValue = 0;
        }

        if (m_nodeWidth.floatValue < 0)
        {
            m_nodeWidth.floatValue = 0;
        }

        if (m_nodeHeight.floatValue < 0)
        {
            m_nodeHeight.floatValue = 0;
        }

        EditorGUILayout.PropertyField(m_width);
        EditorGUILayout.PropertyField(m_height);
        EditorGUILayout.PropertyField(m_nodeWidth);
        EditorGUILayout.PropertyField(m_nodeHeight);

        if (GUILayout.Button("Generate Grid"))
        {
            m_grid.GenerateGrid();
        }

        serializedObject.ApplyModifiedProperties();
    }
}