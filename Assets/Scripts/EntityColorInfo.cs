using UnityEngine;

public class EntityColorInfo : MonoBehaviour
{
    public EntityColor Color
    {
        get
        {
            return m_color;
        }
    }

    [SerializeField]
    private EntityColor m_color;
}