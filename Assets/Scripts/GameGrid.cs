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

    public float NodeWidth
    {
        get
        {
            return m_nodeWidth;
        }
    }

    public float NodeHeight
    {
        get
        {
            return m_nodeHeight;
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

    [SerializeField] 
    private float m_nodeWidth;

    [SerializeField] 
    private float m_nodeHeight;

    [SerializeField]
    [HideInInspector]
    private GameObject[] m_grid;

    private TileFactory m_factory;

    private void Awake()
    {
        m_factory = GetComponent<TileFactory>();
    }

    public void GenerateGrid()
    {
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            GameObject obj = child.gameObject;

            if (Application.isEditor)
            {
                DestroyImmediate(obj);
            }
            else
            {
                Destroy(obj);
            }
        }

        m_grid = new GameObject[NumEntries];

        for (int i = 0; i < NumEntries; i++)
        {
            GameObject prefab = m_factory.GetPrefabForType(TileType.WALL);
            GameObject newObj = Instantiate(prefab, transform);
            GridPosition.Make(newObj, this, (i / Width), (i % Height));
        }
    }

}