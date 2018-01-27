using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class ConnectionVisualization : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    private GameGrid m_grid;

    [SerializeField]
    [HideInInspector]
    private List<Vector3> m_points;

    private LineRenderer m_lineRenderer;

    private void Awake()
    {
        m_points = new List<Vector3>();
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Vector3[] points = m_points.ToArray();
        m_lineRenderer.SetPositions(points);
    }

    private void AddConnection(int lhs, int rhs)
    {
        Vector3 lhsCenter;
        Vector3 rhsCenter;

        if (m_grid.GetCenterFromIndex(lhs, out lhsCenter) && m_grid.GetCenterFromIndex(rhs, out rhsCenter))
        {
            m_points.Add(lhsCenter);
            m_points.Add(rhsCenter);
        }
    }

    public static ConnectionVisualization Make(GameObject owner, GameGrid grid, int lhs, int rhs)
    {
        var component = owner.GetComponent<ConnectionVisualization>();

        if (!component)
        {
            component = owner.AddComponent<ConnectionVisualization>();
            component.m_grid = grid;
        }

        component.AddConnection(lhs, rhs);

        return component;
    }
}