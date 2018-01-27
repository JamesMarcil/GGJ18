using System.Collections.Generic;

using UnityEngine;

public class TileFactory : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TileType> m_keys = new List<TileType>();

    [SerializeField]
    private List<GameObject> m_values = new List<GameObject>();

    private Dictionary<TileType, GameObject> m_tiles = new Dictionary<TileType, GameObject>();

    public bool TryGetPrefabForType(TileType type, out GameObject prefab)
    {
        return m_tiles.TryGetValue(type, out prefab);
    }

    public void OnBeforeSerialize()
    {
        m_keys.Clear();
        m_values.Clear();

        foreach (KeyValuePair<TileType, GameObject> pair in m_tiles)
        {
            m_keys.Add(pair.Key);
            m_values.Add(pair.Value);
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