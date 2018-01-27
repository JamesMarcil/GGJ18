using UnityEngine;
using UnityEngine.UI;

public class SetInteractableOnMessage : MonoBehaviour
{
    [SerializeField]
    private string m_messageToListenFor;

    [SerializeField]
    private bool m_valueToSet;

    [SerializeField]
    private bool m_initialState;

    [SerializeField]
    private Selectable m_target;

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(m_messageToListenFor, OnMessageReceived);
    }

    private void Start()
    {
        m_target.interactable = m_initialState;
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(m_messageToListenFor, OnMessageReceived);
    }

    private void OnMessageReceived(Message msg)
    {
        m_target.interactable = m_valueToSet;
    }
}