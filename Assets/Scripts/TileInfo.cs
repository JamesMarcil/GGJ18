using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public TileType Type
    {
        get
        {
            return m_type;
        }
    }

    [SerializeField]
    private TileType m_type;
}