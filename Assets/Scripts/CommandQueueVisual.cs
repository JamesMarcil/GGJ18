using System;
using System.Collections.Generic;

using UnityEngine;

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
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_SUCCESSFULLY_ENQUEUED, OnEnqueueCommand);
    }

    private void OnEnqueueCommand(Message message)
    {
        var msg = message as CommandMessage;

        BaseCommand command = msg.Command;

        GameObject prefab = m_prefabs[command.Type];
        GameObject newObj = Instantiate(prefab, transform);
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