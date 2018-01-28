using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCommand : MonoBehaviour
{
    public CommandStatus Status
    {
        get
        {
            return m_status;
        }
        protected set
        {
            m_status = value;

            m_onStatusChanged.Invoke(this);
        }
    }

    public StatusEvent OnStatusChanged
    {
        get
        {
            return m_onStatusChanged;
        }
    }

    private CommandStatus m_status = CommandStatus.RUNNING;
    private StatusEvent m_onStatusChanged = new StatusEvent();

    public class StatusEvent : UnityEvent<BaseCommand>
    {
    }
}