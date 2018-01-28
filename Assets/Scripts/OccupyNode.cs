using UnityEngine;

public class OccupyNode : MonoBehaviour
{
    public Node Current
    {
        get
        {
            return m_current;
        }
        set
        {
            m_current = value;

            transform.localPosition = m_current.GetCenter();
        }
    }

    [SerializeField]
    private Node m_current;

    private void OnValidate()
    {
        if (m_current)
        {
            transform.localPosition = m_current.GetCenter();
        }
    }
}