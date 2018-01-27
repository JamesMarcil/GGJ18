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

    private IEnumerable<GameObject> AdjacentNodes()
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