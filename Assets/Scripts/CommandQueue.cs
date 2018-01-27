using System.Collections.Generic;

using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    [SerializeField]
    private int m_capacity;

    [SerializeField]
    [HideInInspector]
    private List<BaseCommand> m_commandQueue = new List<BaseCommand>();

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(GameEvents.ENQUEUE_COMMAND, OnEnqueueCommand);
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.ENQUEUE_COMMAND, OnEnqueueCommand);
    }

    private void OnEnqueueCommand(Message message)
    {
        if (m_commandQueue.Count < m_capacity)
        {
            var msg = message as EnqueueCommandMessage;
            m_commandQueue.Add(msg.Command);
        }
    }
}