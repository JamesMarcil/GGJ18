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

    [SerializeField]
    [HideInInspector]
    private GameObject[] m_grid;

    [SerializeField]
    [HideInInspector]
    private Graph m_graph = new Graph(0);

    [SerializeField]
    private GameObject m_connectionPrefab;

    [SerializeField]
    [HideInInspector]
    private bool m_hasGeneratedGrid;

    [SerializeField]
    [HideInInspector]
    private bool m_hasGeneratedConnectivity;

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

    public bool HasGrid()
    {
        return m_hasGeneratedGrid;
    }

    public bool HasConnectivity()
    {
        return m_hasGeneratedConnectivity;
    }

    public void ClearGrid()
    {
        ClearConnectivity();

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

        m_hasGeneratedGrid = false;
    }

    public void GenerateGrid()
    {
        m_graph = new Graph(NumEntries);

        m_grid = new GameObject[NumEntries];

        for (int i = 0; i < NumEntries; i++)
        {
            GameObject prefab = m_factory.GetPrefabForType(TileType.SPACE);

            GameObject newObj = Instantiate(prefab, transform);

            m_grid[i] = newObj;

            int instanceId = newObj.GetInstanceID();
            m_gameObjects.Add(instanceId, newObj);

            int row;
            int column;
            GetRowAndColumnFromIndex(i, out row, out column);

            GridPosition.Make(newObj, this, row, column);
        }

        m_hasGeneratedGrid = true;
    }

    public bool GetIndexFromRowAndColumn(int row, int column, out int index)
    {
        if ((row < 0) || (column < 0) || (row >= Width) || (column >= Height))
        {
            index = default(int);
            return false;
        }

        index = (row * Width) + column;
        
        return true;
    }

    public bool GetRowAndColumnFromIndex(int index, out int row, out int column)
    {
        if ((index < 0) || (index >= NumEntries))
        {
            row = default(int);
            column = default(int);
            return false;
        }

        row = (index / Width);
        column = (index % Height);
        return true;
    }

    public bool GetCenterFromIndex(int index, out Vector3 center)
    {
        int row;
        int column;
        if (GetRowAndColumnFromIndex(index, out row, out column))
        {
            return GetCenterFromRowAndColumn(row, column, out center);
        }
        else
        {
            center = default(Vector3);
            return false;
        }
    }

    public bool GetCenterFromRowAndColumn(int row, int column, out Vector3 center)
    {
        if ((row < 0) || (column < 0) || (row >= Width) || (column >= Height))
        {
            center = default(Vector3);
            return false;
        }

        center = new Vector3((row * NodeWidth) + (NodeWidth * 0.5f), (column * NodeHeight) + (NodeHeight * 0.5f), 0);
        
        return true;
    }

    private IEnumerable<int> Neighbors(int row, int column)
    {
        int index;

        if (GetIndexFromRowAndColumn(row - 1, column, out index))
        {
            yield return index;
        }

        if (GetIndexFromRowAndColumn(row + 1, column, out index))
        {
            yield return index;
        }

        if (GetIndexFromRowAndColumn(row, column - 1, out index))
        {
            yield return index;
        }

        if (GetIndexFromRowAndColumn(row, column + 1, out index))
        {
            yield return index;
        }
    }

    private bool CanConnect(int lhs, int rhs)
    {
        GameObject lhsObj = m_grid[lhs];
        GameObject rhsObj = m_grid[rhs];

        var lhsInfo = lhsObj.GetComponent<TileInfo>();
        var rhsInfo = rhsObj.GetComponent<TileInfo>();

        return ((lhsInfo.Type != TileType.WALL) && (rhsInfo.Type != TileType.WALL));
    }

    public void ClearConnectivity()
    {
        m_graph.Clear();

        GameObject[] connections = GameObject.FindGameObjectsWithTag("Connection");
        for (int i = 0; i < connections.Length; i++)
        {
            GameObject obj = connections[i];
            if (Application.isEditor)
            {
                DestroyImmediate(obj);
            }
            else
            {
                Destroy(obj);
            }
        }

        m_hasGeneratedConnectivity = false;
    }

    public void GenerateConnectivity()
    {
        ClearConnectivity();

        for (int lhs = 0; lhs < NumEntries; lhs++)
        {
            int row;
            int column;
            GetRowAndColumnFromIndex(lhs, out row, out column);

            foreach (int rhs in Neighbors(row, column))
            {
                if (CanConnect(lhs, rhs))
                {
                    GameObject obj = Instantiate(m_connectionPrefab, transform);

                    ConnectionVisualization.Make(obj, this, lhs, rhs);

                    m_graph.Connect(lhs, rhs);
                }
            }
        }

        m_hasGeneratedConnectivity = true;
    }
}