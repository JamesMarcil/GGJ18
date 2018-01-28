using UnityEngine.Events;

public class CommandStatusManager
{
    public CommandStatus Value
    {
        get
        {
            return m_status;
        }
        set
        {
            m_status = value;

            OnStatusChanged.Invoke(m_status);
        }
    }

    public StatusEvent OnStatusChanged { get; private set; }

    private CommandStatus m_status;

    public CommandStatusManager()
    {
        OnStatusChanged = new StatusEvent();

        m_status = CommandStatus.RUNNING;
    }

    public class StatusEvent : UnityEvent<CommandStatus>
    {
    }
}