using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Graph
{
    [SerializeField]
    private int m_numEntries;

    [SerializeField]
    private bool[] m_adjacencyMatrix;

    public Graph(int numEntries)
    {
        m_numEntries = numEntries;

        m_adjacencyMatrix = new bool[m_numEntries * m_numEntries];
        for (int i = 0; i < m_numEntries; i++)
        {
            int index;
            if (GetIndexFromRowAndColumn(i, i, out index))
            {
                m_adjacencyMatrix[index] = true;
            }
        }
    }

    public bool GetRowAndColumnFromIndex(int index, out int row, out int column)
    {
        if ((index < 0) || (index >= m_numEntries))
        {
            row = default(int);
            column = default(int);

            return false;
        }

        row = (index / m_numEntries);
        column = (index % m_numEntries);

        return true;
    }

    private bool GetIndexFromRowAndColumn(int row, int column, out int index)
    {
        if ((row < 0) || (row >= m_numEntries) || (column < 0) || (column >= m_numEntries))
        {
            index = default(int);
            return false;
        }

        index = (row * m_numEntries) + column;

        return true;
    }

    public bool IsConnected(int lhs, int rhs)
    {
        int lhsIndex;
        int rhsIndex;

        GetIndexFromRowAndColumn(lhs, rhs, out lhsIndex);
        GetIndexFromRowAndColumn(rhs, lhs, out rhsIndex);

        return (m_adjacencyMatrix[lhsIndex] || m_adjacencyMatrix[rhsIndex]);
    }

    public void Clear()
    {
        for (int i = 0; i < m_numEntries; i++)
        {
            for (int j = i; j < m_numEntries; j++)
            {
                if (i == j)
                {
                    continue;
                }

                int lhsIndex;
                int rhsIndex;

                GetIndexFromRowAndColumn(i, j, out lhsIndex);
                GetIndexFromRowAndColumn(j, i, out rhsIndex);

                m_adjacencyMatrix[lhsIndex] = false;
                m_adjacencyMatrix[rhsIndex] = false;
            }
        }
    }

    public void Connect(int lhs, int rhs)
    {
        int lhsIndex;
        int rhsIndex;

        GetIndexFromRowAndColumn(lhs, rhs, out lhsIndex);
        GetIndexFromRowAndColumn(rhs, lhs, out rhsIndex);

        m_adjacencyMatrix[lhsIndex] = true;
        m_adjacencyMatrix[rhsIndex] = true;
    }

    public void Disconnect(int lhs, int rhs)
    {
        int lhsIndex;
        int rhsIndex;

        GetIndexFromRowAndColumn(lhs, rhs, out lhsIndex);
        GetIndexFromRowAndColumn(rhs, lhs, out rhsIndex);

        m_adjacencyMatrix[lhsIndex] = false;
        m_adjacencyMatrix[rhsIndex] = false;
    }

    public IEnumerable<int> Neighbors(int id)
    {
        for (int i = 0; i < m_numEntries; i++)
        {
            int index;

            GetIndexFromRowAndColumn(id, i, out index);

            if (m_adjacencyMatrix[index])
            {
                yield return i;
            }
        }
    }
}