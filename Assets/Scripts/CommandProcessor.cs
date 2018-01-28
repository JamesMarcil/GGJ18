using UnityEngine;

[RequireComponent(typeof(CommandQueue))]
public class CommandProcessor : MonoBehaviour
{
    private BaseCommand CurrentCommand
    {
        get
        {
            return m_commandQueue.Queue[m_currentIndex];
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
            m_currentIndex = Mathf.Min(Mathf.Max(0, value), m_commandQueue.Queue.Count);

            if (m_currentIndex >= m_commandQueue.Queue.Count)
            {
                enabled = false;
            }
        }
    }

    private int m_currentIndex;

    private CommandQueue m_commandQueue;

    private void Awake()
    {
        m_commandQueue = GetComponent<CommandQueue>();
    }

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