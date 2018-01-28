using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Node : MonoBehaviour
{
    public int Id 
    {
        get
        {
            return m_id;
        }
    }

    public TileType Type
    {
        get
        {
            return m_type;
        }
    }

    [SerializeField]
    [HideInInspector]
    private GameGrid m_grid;

    [SerializeField]
    [HideInInspector]
    private TileType m_type;

    [SerializeField]
    [HideInInspector]
    private int m_id;

    public NodeEvent OnEnterNode { get; private set; }
    public NodeEvent OnExitNode { get; private set; }

    private void Awake()
    {
        OnEnterNode = new NodeEvent();
        OnExitNode = new NodeEvent();
    }

    public bool GetAdjacentInDirection(Directions direction, out GameObject obj)
    {
        obj = default(GameObject);

        int row;
        int column;
        if (m_grid.GetRowAndColumnFromIndex(m_id, out row, out column))
        {
            int targetRow;
            int targetColumn;
            int targetIndex;

            switch (direction)
            {
                case Directions.NORTH:
                    {
                        targetRow = (row + 1);
                        targetColumn = column;
                        break;
                    }
                case Directions.EAST:
                    {
                        targetRow = row;
                        targetColumn = (column + 1);
                        break;
                    }
                case Directions.SOUTH:
                    {
                        targetRow = (row - 1);
                        targetColumn = column;
                        break;
                    }
                case Directions.WEST:
                    {
                        targetRow = row;
                        targetColumn = (column - 1);
                        break;
                    }
                default:
                    {
                        obj = default(GameObject);
                        return false;
                    }
            }

            return m_grid.GetIndexFromRowAndColumn(targetRow, targetColumn, out targetIndex) && m_grid.GetGameObjectFromIndex(targetIndex, out obj);
        }
        
        return false;
    }

    public bool GetConnectedInDirection(Directions direction, out GameObject obj)
    {
        if (GetAdjacentInDirection(direction, out obj))
        {
            var component = obj.GetComponent<Node>();
            if (IsConnected(component))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerable<GameObject> AdjacentNodes()
    {
        foreach (int index in m_grid.AdjacentFromIndex(m_id))
        {
            GameObject obj;
            if (m_grid.GetGameObjectFromIndex(index, out obj))
            {
                yield return obj;
            }
        }
    }

    public IEnumerable<GameObject> ConnectedNodes()
    {
        foreach (int index in m_grid.ConnectedNeighborsFromIndex(m_id))
        {
            GameObject obj;
            if (m_grid.GetGameObjectFromIndex(index, out obj))
            {
                yield return obj;
            }
        }
    }

    public Vector3 GetCenter()
    {
        Vector3 center;
        m_grid.GetCenterFromIndex(m_id, out center);
        
        return center;
    }

    public bool IsConnected(Node rhs)
    {
        return m_grid.IsConnected(m_id, rhs.m_id);
    }

    public static Node Make(GameObject owner, GameGrid grid, int index, TileType type)
    {
        var component = owner.AddComponent<Node>();
        component.m_grid = grid;
        component.m_id = index;
        component.m_type = type;
        return component;
    }

    public class NodeEvent : UnityEvent<GameObject>
    {
    }

}