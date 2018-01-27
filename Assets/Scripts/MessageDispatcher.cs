using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class MessageDispatcher
{
    public static readonly MessageDispatcher Instance = new MessageDispatcher();

    private Dictionary<string, UnityMessage> m_events;

    private MessageDispatcher()
    {
        m_events = new Dictionary<string, UnityMessage>();
    }

    public void AddListener(string type, UnityAction<Message> listener)
    {
        UnityMessage message;
        if (m_events.TryGetValue(type, out message))
        {
            message.AddListener(listener);
        }
        else
        {
            message = new UnityMessage();
            message.AddListener(listener);
            m_events.Add(type, message);
        }
    }

    public void RemoveListener(string type, UnityAction<Message> listener)
    {
        UnityMessage message;
        if (m_events.TryGetValue(type, out message))
        {
            message.RemoveListener(listener);
        }
    }

    public void DispatchMessage(Message msg)
    {
        UnityMessage message;
        if (m_events.TryGetValue(msg.Type, out message))
        {
            message.Invoke(msg);
        }
    }
}