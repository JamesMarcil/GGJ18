using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TileFactory))]
public class GameGrid : MonoBehaviour
{
    public int Width
    {
        get
        {
            return m_width;
        }
    }

    public int Height
    {
        get
        {
            return m_height;
        }
    }

    public int NumEntries
    {
        get
        {
            return (m_width * m_height);
        }
    }

    [SerializeField]
    private int m_width;

    [SerializeField]
    private int m_height;

    private TileFactory m_factory;

    private GameObject[] m_grid;

    private void Awake()
    {
        m_factory = GetComponent<TileFactory>();
    }

    public void GenerateGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }

        m_grid = new GameObject[NumEntries];

        for (int i = 0; i < NumEntries; i++)
        {
            GameObject prefab = m_factory.GetPrefabForType(TileType.WALL);
            GameObject obj = Instantiate(prefab, transform);
        }
    }
}