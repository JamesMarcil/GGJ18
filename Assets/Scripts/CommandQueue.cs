using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    public ReadOnlyCollection<BaseCommand> Queue
    {
        get
        {
            return m_commandQueue.AsReadOnly();
        }
    }

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
        var msg = message as CommandMessage;

        if (m_commandQueue.Count < m_capacity)
        {
            m_commandQueue.Add(msg.Command);

            var successMsg = new CommandMessage(GameEvents.COMMAND_SUCCESSFULLY_ENQUEUED, msg.Command);
            MessageDispatcher.Instance.DispatchMessage(successMsg);

            if (m_commandQueue.Count >= m_capacity)
            {
                var atCapacityMsg = new Message(GameEvents.COMMAND_QUEUE_AT_CAPACITY);
                MessageDispatcher.Instance.DispatchMessage(atCapacityMsg);
            }
        }
        else
        {
            var failedMsg = new CommandMessage(GameEvents.COMMAND_FAILED_TO_ENQUEUE, msg.Command);
            MessageDispatcher.Instance.DispatchMessage(failedMsg);
        }
    }
}