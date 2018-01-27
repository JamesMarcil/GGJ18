using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TileFactory))]
public class GameGrid : MonoBehaviour, ISerializationCallbackReceiver
{
    public int Width
    {
        get
        {
            return m_width;
        }
    }

    public int Height
    {
        get
        {
            return m_height;
        }
    }

    public float NodeWidth
    {
        get
        {
            return m_nodeWidth;
        }
    }

    public float NodeHeight
    {
        get
        {
            return m_nodeHeight;
        }
    }

    public int NumEntries
    {
        get
        {
            return (m_width * m_height);
        }
    }

    [SerializeField]
    private int m_width;

    [SerializeField]
    private int m_height;

    [SerializeField]
    private float m_nodeWidth;

    [SerializeField]
    private float m_nodeHeight;

    [SerializeField]
    [HideInInspector]
    private List<int> m_keys = new List<int>();

    [SerializeField]
    [HideInInspector]
    private List<GameObject> m_values = new List<GameObject>();

    private Dictionary<int, GameObject> m_gameObjects = new Dictionary<int, GameObject>();

    private TileFactory m_factory;

    private void Awake()
    {
        m_factory = GetComponent<TileFactory>();
    }

    public void OnBeforeSerialize()
    {
        m_keys.Clear();
        m_values.Clear();

        foreach (KeyValuePair<int, GameObject> pair in m_gameObjects)
        {
            m_keys.Add(pair.Key);
            m_values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        m_gameObjects.Clear();

        int count = Mathf.Min(m_keys.Count, m_values.Count);
        for (int i = 0; i < count; i++)
        {
            int key = m_keys[i];
            GameObject value = m_values[i];
            m_gameObjects.Add(key, value);
        }
    }

    public void ClearGrid()
    {
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            GameObject obj = child.gameObject;

            if (Application.isEditor)
            {
                DestroyImmediate(obj);
            }
            else
            {
                Destroy(obj);
            }
        }

        m_keys.Clear();
        m_values.Clear();
        m_gameObjects.Clear();
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < NumEntries; i++)
        {
            GameObject prefab = m_factory.GetPrefabForType(TileType.WALL);

            GameObject newObj = Instantiate(prefab, transform);

            int instanceId = newObj.GetInstanceID();

            m_gameObjects.Add(instanceId, newObj);

            GridPosition.Make(newObj, this, (i / Width), (i % Height));
        }
    }

}