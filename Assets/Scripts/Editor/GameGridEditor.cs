using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGrid))]
public class GameGridEditor : Editor
{
    private SerializedProperty m_width;
    private SerializedProperty m_height;
    private SerializedProperty m_cellWidth;
    private SerializedProperty m_cellHeight;

    private GameGrid m_grid;

    private void OnEnable()
    {
        m_width = serializedObject.FindProperty("m_width");
        m_height = serializedObject.FindProperty("m_height");
        m_cellWidth = serializedObject.FindProperty("m_cellWidth");
        m_cellHeight = serializedObject.FindProperty("m_cellHeight");

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

        if (m_cellWidth.floatValue < 0)
        {
            m_cellWidth.floatValue = 0;
        }

        if (m_cellHeight.floatValue < 0)
        {
            m_cellHeight.floatValue = 0;
        }

        EditorGUILayout.PropertyField(m_width);
        EditorGUILayout.PropertyField(m_height);
        EditorGUILayout.PropertyField(m_cellWidth);
        EditorGUILayout.PropertyField(m_cellHeight);

        if (GUILayout.Button("Generate Grid"))
        {
            m_grid.GenerateGrid();
        }

        serializedObject.ApplyModifiedProperties();
    }
}