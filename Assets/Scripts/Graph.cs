using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Graph
{
    [SerializeField]
    private int m_numEntries;

    [SerializeField]
    private bool[,] m_adjacencyMatrix;

    public Graph(int numEntries)
    {
        m_numEntries = numEntries;

        m_adjacencyMatrix = new bool[m_numEntries, m_numEntries];
        for (int i = 0; i < m_numEntries; i++)
        {
            m_adjacencyMatrix[i, i] = true;
        }
    }

    public bool IsConnected(int lhs, int rhs)
    {
        return (m_adjacencyMatrix[lhs, rhs] || m_adjacencyMatrix[rhs, lhs]);
    }

    public void Connect(int lhs, int rhs)
    {
        m_adjacencyMatrix[lhs, rhs] = true;
        m_adjacencyMatrix[rhs, lhs] = true;
    }

    public void Disconnect(int lhs, int rhs)
    {
        m_adjacencyMatrix[lhs, rhs] = false;
        m_adjacencyMatrix[rhs, lhs] = false;
    }

    public IEnumerable<int> Neighbors(int id)
    {
        for (int i = 0; i < m_numEntries; i++)
        {
            if (m_adjacencyMatrix[id, i])
            {
                yield return i;
            }
        }
    }
}