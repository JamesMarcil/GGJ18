using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGrid))]
public class GameGridEditor : Editor
{
    private SerializedProperty m_width;
    private SerializedProperty m_height;

    private GameGrid m_grid;

    private void OnEnable()
    {
        m_width = serializedObject.FindProperty("m_width");
        m_height = serializedObject.FindProperty("m_height");

        m_grid = target as GameGrid;
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_width);

        EditorGUILayout.PropertyField(m_height);

        if (GUILayout.Button("Generate Grid"))
        {
            m_grid.GenerateGrid();
        }

        serializedObject.ApplyModifiedProperties();
    }
}