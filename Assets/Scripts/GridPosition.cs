using UnityEngine;

[ExecuteInEditMode]
public class GridPosition : MonoBehaviour
{
    public int Row
    {
        get
        {
            return m_row;
        }
    }

    public int Column
    {
        get
        {
            return m_column;
        }
    }

    [SerializeField]
    [HideInInspector]
    private GameGrid m_grid;

    [SerializeField]
    private int m_row;

    [SerializeField]
    private int m_column;

    private void Awake()
    {
        transform.hideFlags = HideFlags.NotEditable;
    }

    private void Update()
    {
        Vector2 position = Vector2.zero;
        position.x = m_row * m_grid.NodeWidth;
        position.y = m_column * m_grid.NodeHeight;
        transform.localPosition = position;
    }

    public static GridPosition Make(GameObject owner, GameGrid grid, int row, int column)
    {
        var component = owner.AddComponent<GridPosition>();
        component.m_row = row;
        component.m_column = column;
        component.m_grid = grid;
        return component;
    }
}
