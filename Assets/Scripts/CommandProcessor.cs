using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    private BaseCommand CurrentCommand
    {
        get
        {
            return m_commands[m_currentIndex];
        }
    }

    private int CurrentIndex
    {
        get
        {
            return m_currentIndex;
        }
        set 
        {
            m_currentIndex = Mathf.Min(Mathf.Max(0, value), m_commands.Length);

            if (m_currentIndex >= m_commands.Length)
            {
                enabled = false;
            }
        }
    }

    private int m_currentIndex;

    [SerializeField]
    private BaseCommand[] m_commands;

    private void Update()
    {
        if (!CurrentCommand.IsStarted)
        {
            CurrentCommand.OnStart(gameObject);
        }

        CommandStatus status = CurrentCommand.OnUpdated(gameObject);

        switch (status)
        {
            case CommandStatus.COMPLETE:
                {
                    CurrentCommand.OnStop(gameObject);

                    CurrentIndex += 1;

                    break;
                }
            case CommandStatus.FAILURE:
                {
                    CurrentCommand.OnStop(gameObject);

                    enabled = false;

                    break;
                }
            default:
                {
                    break; // No-op
                }
        }
    }
}