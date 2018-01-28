using System.Text;
using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TileFactory))]
public class GameGrid : MonoBehaviour
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

        m_hasGeneratedGrid = false;
    }

    public void GenerateGrid()
    {
        m_hasGeneratedGrid = true;

        m_graph = new Graph(NumEntries);
        m_grid = new GameObject[NumEntries];

        for (int i = 0; i < NumEntries; i++)
        {
            MakeTile(TileType.WALL, i);
        }

        GenerateConnectivity();
    }

    public bool IsValidIndex(int index)
    {
        return (index >= 0) && (index < NumEntries);
    }

    public bool IsValidRow(int row)
    {
        return (row >= 0) && (row < Width);
    }

    public bool IsValidColumn(int column)
    {
        return (column >= 0) && (column < Height);
    }

    public bool IsValidRowAndColumn(int row, int column)
    {
        return IsValidRow(row) && IsValidColumn(column);
    }

    public bool GetIndexFromRowAndColumn(int row, int column, out int index)
    {
        if (IsValidRowAndColumn(row, column))
        {
            index = (row * Width) + column;
            return true;
        }
        else
        {
            index = default(int);
            return false;
        }
    }

    public bool GetRowAndColumnFromIndex(int index, out int row, out int column)
    {
        if (IsValidIndex(index))
        {
            row = (index / Width);
            column = (index % Height);
            return true;
        }
        else
        {
            row = default(int);
            column = default(int);
            return false;
        }
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
        if (IsValidRowAndColumn(row, column))
        {
            center = new Vector3((row * NodeWidth) + (NodeWidth * 0.5f), (column * NodeHeight) + (NodeHeight * 0.5f), 0);
            return true;
        }
        else
        {
            center = default(Vector3);
            return false;
        }
    }

    public bool GetGameObjectFromIndex(int index, out GameObject obj)
    {
        if (IsValidIndex(index))
        {
            obj = m_grid[index];
            return true;
        }
        else
        {
            obj = default(GameObject);
            return false;
        }
    }

    public bool GetGameObjectFromRowAndColumn(int row, int column, out GameObject obj)
    {
        int index;
        if (GetIndexFromRowAndColumn(row, column, out index))
        {
            return GetGameObjectFromIndex(index, out obj);
        }
        else
        {
            obj = default(GameObject);
            return false;
        }
    }

    public IEnumerable<int> ConnectedNeighborsFromIndex(int lhsIndex)
    {
        int row;
        int column;
        GetRowAndColumnFromIndex(lhsIndex, out row, out column);

        foreach (int index in ConnectedNeighborsFromRowAndColumn(row, column))
        {
            yield return index;
        }
    }

    public IEnumerable<int> ConnectedNeighborsFromRowAndColumn(int row, int column)
    {
        int lhsIndex;
        GetIndexFromRowAndColumn(row, column, out lhsIndex);

        foreach (int rhsIndex in AdjacentFromRowAndColumn(row, column))
        {
            if (m_graph.IsConnected(lhsIndex, rhsIndex))
            {
                yield return rhsIndex;
            }
        }
    }

    public IEnumerable<int> AdjacentFromIndex(int index)
    {
        int row;
        int column;
        GetRowAndColumnFromIndex(index, out row, out column);

        foreach (int adjacent in AdjacentFromRowAndColumn(row, column))
        {
            yield return adjacent;
        }
    }

    public IEnumerable<int> AdjacentFromRowAndColumn(int row, int column)
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

    public bool IsConnected(int lhs, int rhs)
    {
        if (IsValidIndex(lhs) && IsValidIndex(rhs))
        {
            return m_graph.IsConnected(lhs, rhs);
        }

        return false;
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
        m_hasGeneratedConnectivity = true;

        for (int lhs = 0; lhs < NumEntries; lhs++)
        {
            int row;
            int column;
            GetRowAndColumnFromIndex(lhs, out row, out column);

            foreach (int rhs in AdjacentFromRowAndColumn(row, column))
            {
                if (CanConnect(lhs, rhs))
                {
                    GameObject obj = Instantiate(m_connectionPrefab, transform);

                    ConnectionVisualization.Make(obj, this, lhs, rhs);

                    m_graph.Connect(lhs, rhs);
                }
            }
        }
    }

    private GameObject MakeTile(TileType type, int index)
    {
        GameObject prefab = m_factory.GetPrefabForType(type);
        GameObject newObj = Instantiate(prefab, transform);

        m_grid[index] = newObj;

        int row;
        int column;
        GetRowAndColumnFromIndex(index, out row, out column);

        var buffer = new StringBuilder();
        buffer.Append(row);
        buffer.Append("_");
        buffer.Append(column);
        newObj.name = buffer.ToString();

        Node.Make(newObj, this, index, type);
        ReplaceNode.Make(newObj, this);
        GridPosition.Make(newObj, this, row, column);

        return newObj;
    }

    public Node ReplaceWithTileOfType(Node node, TileType type)
    {
        if (IsValidIndex(node.Id))
        {
            GameObject newObj = MakeTile(type, node.Id);
            Node newNode = newObj.GetComponent<Node>();

            int siblingIndex = node.gameObject.transform.GetSiblingIndex();
            newObj.transform.SetSiblingIndex(siblingIndex);

            if (Application.isEditor)
            {
                DestroyImmediate(node.gameObject);
            }
            else
            {
                Destroy(node.gameObject);
            }

            ClearConnectivity();
            GenerateConnectivity();

            return newNode;
        }

        return node;
    }
}