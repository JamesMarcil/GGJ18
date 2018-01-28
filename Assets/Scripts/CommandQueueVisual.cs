using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CommandQueueVisual : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    [HideInInspector]
    private List<CommandTypes> m_keys = new List<CommandTypes>();

    [SerializeField]
    [HideInInspector]
    private List<GameObject> m_values = new List<GameObject>();

    private Dictionary<CommandTypes, GameObject> m_prefabs = new Dictionary<CommandTypes, GameObject>();

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_SUCCESSFULLY_ENQUEUED, OnEnqueueCommand);
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_REMOVE_COMMAND, OnRemoveCommand);
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_PROCESSING_COMMAND, OnProcessingCommand);
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_SUCCESSFULLY_PROCESSED_COMMAND, OnSuccessfullyProcessed);
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_FAILED_TO_PROCESS_COMMAND, OnFailedToProcess);
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_SUCCESSFULLY_ENQUEUED, OnEnqueueCommand);
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_REMOVE_COMMAND, OnRemoveCommand);
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_PROCESSING_COMMAND, OnProcessingCommand);
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_SUCCESSFULLY_PROCESSED_COMMAND, OnSuccessfullyProcessed);
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_FAILED_TO_PROCESS_COMMAND, OnFailedToProcess);
    }

    private void OnEnqueueCommand(Message message)
    {
        var msg = message as CommandMessage;

        BaseCommand command = msg.Command;

        GameObject prefab = m_prefabs[command.Type];
        Instantiate(prefab, transform);
    }

    private void OnRemoveCommand(Message message)
    {
        var msg = message as CommandAtIndexMessage;

        Transform child = transform.GetChild(msg.Index + 1);
        GameObject obj = child.gameObject;
        Destroy(obj);
    }

    private void OnProcessingCommand(Message message)
    {
        var msg = message as CommandAtIndexMessage;

        Transform child = transform.GetChild(msg.Index + 1);
        GameObject obj = child.gameObject;

        var component = obj.GetComponent<Image>();
        component.color = Color.yellow;
    }

    private void OnSuccessfullyProcessed(Message message)
    {
        var msg = message as CommandAtIndexMessage;

        Transform child = transform.GetChild(msg.Index + 1);
        GameObject obj = child.gameObject;

        var component = obj.GetComponent<Image>();
        component.color = Color.green;
    }

    private void OnFailedToProcess(Message message)
    {
        var msg = message as CommandAtIndexMessage;

        Transform child = transform.GetChild(msg.Index + 1);
        GameObject obj = child.gameObject;

        var component = obj.GetComponent<Image>();
        component.color = Color.red;
    }

    public void OnBeforeSerialize()
    {
        m_keys.Clear();
        m_values.Clear();

        CommandTypes[] types = Enum.GetValues(typeof(CommandTypes)) as CommandTypes[];
        for (int i = 0; i < types.Length; i++)
        {
            CommandTypes type = types[i];

            m_keys.Add(type);

            if (m_prefabs.ContainsKey(type))
            {
                m_values.Add(m_prefabs[type]);
            }
            else
            {
                m_values.Add(null);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        m_prefabs.Clear();

        int count = Mathf.Min(m_keys.Count, m_values.Count);
        for (int i = 0; i < count; i++)
        {
            CommandTypes key = m_keys[i];
            GameObject value = m_values[i];
            m_prefabs.Add(key, value);
        }
    }
}