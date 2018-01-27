using System;
using System.Collections.Generic;

using UnityEngine;

public class TileFactory : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TileType> m_keys = new List<TileType>();

    [SerializeField]
    private List<GameObject> m_values = new List<GameObject>();

    private Dictionary<TileType, GameObject> m_tiles = new Dictionary<TileType, GameObject>();

    public GameObject GetPrefabForType(TileType type)
    {
        return m_tiles[type];
    }

    public void OnBeforeSerialize()
    {
        m_keys.Clear();
        m_values.Clear();

        TileType[] types = Enum.GetValues(typeof(TileType)) as TileType[];
        for (int i = 0; i < types.Length; i++)
        {
            TileType type = types[i];

            m_keys.Add(type);

            if (m_tiles.ContainsKey(type))
            {
                m_values.Add(m_tiles[type]);
            }
            else
            {
                m_values.Add(null);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        m_tiles.Clear();

        int count = Mathf.Min(m_keys.Count, m_values.Count);
        for (int i = 0; i < count; i++)
        {
            m_tiles.Add(m_keys[i], m_values[i]);
        }
    }
}