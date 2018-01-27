using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TileInfo))]
public class ReplaceTile : MonoBehaviour
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

    private TileInfo m_info;

    private void Awake()
    {
        m_info = GetComponent<TileInfo>();
    }

    public void ReplaceWithType(TileType type)
    {
        m_grid.ReplaceWithTileOfType(gameObject, type);
    }

    public static ReplaceTile Make(GameObject owner, GameGrid grid)
    {
        var component = owner.AddComponent<ReplaceTile>();
        component.m_grid = grid;
        return component;
    }
}