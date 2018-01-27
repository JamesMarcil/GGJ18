using UnityEngine;

[ExecuteInEditMode]
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

    private void Awake()
    {
        hideFlags = HideFlags.NotEditable;
    }
}