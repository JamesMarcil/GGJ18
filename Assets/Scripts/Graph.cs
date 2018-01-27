using System;

using UnityEngine;

[Serializable]
public class Graph
{
    [SerializeField]
    private int m_width;

    [SerializeField]
    private int m_height;

    [SerializeField]
    private bool[,] m_adjacencyMatrix;

    public Graph(int width, int height)
    {
        m_width = width;
        m_height = height;
        m_adjacencyMatrix = new bool[m_width, m_height];
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
}