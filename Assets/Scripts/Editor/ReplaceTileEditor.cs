using System;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReplaceTile))]
public class ReplaceTileEditor : Editor
{
    private ReplaceTile m_replaceTile;

    private void Awake()
    {
        m_replaceTile = target as ReplaceTile;
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
                m_replaceTile.ReplaceWithType(type);
            }
        }
    }
}