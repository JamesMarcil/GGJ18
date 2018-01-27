using UnityEngine;

public class SetBehaviourEnabledOnMessage : MonoBehaviour
{
    [SerializeField]
    private string m_messageToListenFor;

    [SerializeField]
    private bool m_valueToSet;

    [SerializeField]
    private bool m_initialState;

    [SerializeField]
    private Behaviour m_target;

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(m_messageToListenFor, OnMessageReceived);
    }

    private void Start()
    {
        m_target.enabled = m_initialState;
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(m_messageToListenFor, OnMessageReceived);
    }

    private void OnMessageReceived(Message msg)
    {
        m_target.enabled = m_valueToSet;
    }
}