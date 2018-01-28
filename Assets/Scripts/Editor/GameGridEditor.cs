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
    private SerializedProperty m_connectionPrefab;

    private GameGrid m_grid;

    private void OnEnable()
    {
        m_width = serializedObject.FindProperty("m_width");
        m_height = serializedObject.FindProperty("m_height");
        m_nodeWidth = serializedObject.FindProperty("m_nodeWidth");
        m_nodeHeight = serializedObject.FindProperty("m_nodeHeight");
        m_connectionPrefab = serializedObject.FindProperty("m_connectionPrefab");

        m_grid = target as GameGrid;
    }

    private void GenerateGrid()
    {
        m_grid.GenerateGrid();
    }

    private void GenerateConnectivity()
    {
        m_grid.GenerateConnectivity();
    }

    private void ClearGrid()
    {
        m_grid.ClearGrid();
    }

    private void ClearConnectivity()
    {
        m_grid.ClearConnectivity();
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

        EditorGUI.BeginDisabledGroup(m_grid.HasGrid());
        EditorGUILayout.PropertyField(m_width);
        EditorGUILayout.PropertyField(m_height);
        EditorGUILayout.PropertyField(m_nodeWidth);
        EditorGUILayout.PropertyField(m_nodeHeight);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.PropertyField(m_connectionPrefab);

        EditorGUI.BeginDisabledGroup(m_grid.HasGrid());
        if (GUILayout.Button("Generate New Grid"))
        {
            GenerateGrid();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!m_grid.HasGrid());
        if (GUILayout.Button("Generate Connectivity"))
        {
            GenerateConnectivity();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!m_grid.HasGrid());
        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!m_grid.HasConnectivity());
        if (GUILayout.Button("Clear Connectivity"))
        {
            ClearConnectivity();
        }
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}