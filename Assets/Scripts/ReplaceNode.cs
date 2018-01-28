using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Node))]
[RequireComponent(typeof(TileInfo))]
public class ReplaceNode : MonoBehaviour
{
    public TileType Type
    {
        get
        {
            return m_info.Type;
        }
    }

    [SerializeField]
    [HideInInspector]
    private GameGrid m_grid;

    private Node m_node;
    private TileInfo m_info;

    private void Awake()
    {
        m_node = GetComponent<Node>();
        m_info = GetComponent<TileInfo>();
    }

    public Node ReplaceWithType(TileType type)
    {
        return m_grid.ReplaceWithTileOfType(m_node, type);
    }

    public static ReplaceNode Make(GameObject owner, GameGrid grid)
    {
        var component = owner.AddComponent<ReplaceNode>();
        component.m_grid = grid;
        return component;
    }
}