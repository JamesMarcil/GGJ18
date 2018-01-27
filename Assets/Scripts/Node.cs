using System.Collections.Generic;

using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    private GameGrid m_grid;

    [SerializeField]
    [HideInInspector]
    private int m_index;

    public bool GetNodeInDirection(Directions direction, out GameObject obj)
    {
        int row;
        int column;
        if (m_grid.GetRowAndColumnFromIndex(m_index, out row, out column))
        {
            switch (direction)
            {
                case Directions.NORTH:
                    {
                        return m_grid.GetGameObjectFromRowAndColumn(row + 1, column, out obj);
                    }
                case Directions.EAST:
                    {
                        return m_grid.GetGameObjectFromRowAndColumn(row, column + 1, out obj);
                    }
                case Directions.SOUTH:
                    {
                        return m_grid.GetGameObjectFromRowAndColumn(row - 1, column, out obj);
                    }
                case Directions.WEST:
                    {
                        return m_grid.GetGameObjectFromRowAndColumn(row, column - 1, out obj);
                    }
                default:
                    {
                        break; // No-op
                    }
            }
        }

        obj = default(GameObject);
        return false;
    }

    public IEnumerable<GameObject> AdjacentNodes()
    {
        foreach (int index in m_grid.AdjacentFromIndex(m_index))
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
        foreach (int index in m_grid.ConnectedNeighborsFromIndex(m_index))
        {
            GameObject obj;
            if (m_grid.GetGameObjectFromIndex(index, out obj))
            {
                yield return obj;
            }
        }
    }

    public static Node Make(GameObject owner, GameGrid grid, int index)
    {
        var component = owner.AddComponent<Node>();
        component.m_grid = grid;
        component.m_index = index;
        return component;
    }
}