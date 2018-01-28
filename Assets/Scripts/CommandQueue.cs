using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    public ReadOnlyCollection<BaseCommandAsset> Queue
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
    private List<BaseCommandAsset> m_commandQueue = new List<BaseCommandAsset>();

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_ENQUEUE_COMMAND, OnEnqueueCommand);
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_REMOVE_COMMAND, OnRemoveCommand);
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_ENQUEUE_COMMAND, OnEnqueueCommand);
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_REMOVE_COMMAND, OnRemoveCommand);
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

    private void OnRemoveCommand(Message message)
    {
        var msg = message as CommandAtIndexMessage;

        if ((msg.Index >= 0) && (msg.Index < m_commandQueue.Count))
        {
            m_commandQueue.RemoveAt(msg.Index);

            if (m_commandQueue.Count <= 0)
            {
                var newMsg = new Message(GameEvents.COMMAND_QUEUE_EMPTY);
                MessageDispatcher.Instance.DispatchMessage(newMsg);
            }
        }
    }
}