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
            Node previous = m_current;

            m_current = value;

            Node current = value;

            if (previous)
            {
                previous.OnExitNode.Invoke(gameObject);
            }

            if (current)
            {
                current.OnEnterNode.Invoke(gameObject);

                transform.localPosition = m_current.GetCenter();
            }
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