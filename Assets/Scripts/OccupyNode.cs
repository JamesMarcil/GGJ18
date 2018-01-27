using UnityEngine;

[ExecuteInEditMode]
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

            OnValidate();
        }
    }

    [SerializeField]
    private Node m_current;

    private void OnValidate()
    {
        transform.localPosition = m_current.GetCenter();
    }
}