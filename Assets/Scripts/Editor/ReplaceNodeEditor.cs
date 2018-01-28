using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReplaceNode))]
public class ReplaceNodeEditor : Editor
{
    private ReplaceNode m_replaceTile;

    private void Awake()
    {
        m_replaceTile = target as ReplaceNode;
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

        TileType[] types = Enum.GetValues(typeof(TileType)) as TileType[];
        for (int i = 0; i < types.Length; i++)
        {
            TileType type = types[i];

            if (type == m_replaceTile.Type)
            {
                continue;
            }

            if (GUILayout.Button("Replace with " + type))
            {
                Node newNode = m_replaceTile.ReplaceWithType(type);

                Selection.activeGameObject = newNode.gameObject;
                EditorApplication.ExecuteMenuItem("Window/Hierarchy");
            }
        }
    }
}