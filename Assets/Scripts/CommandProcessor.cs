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
            m_currentIndex = Mathf.Min(Mathf.Max(0, value), m_commandQueue.Queue.Count - 1);

            if (value >= m_commandQueue.Queue.Count)
            {
                var stopMsg = new Message(GameEvents.COMMAND_QUEUE_STOP_PROCESSING);
                MessageDispatcher.Instance.DispatchMessage(stopMsg);
            }
        }
    }

    private int m_currentIndex;

    private CommandQueue m_commandQueue;

    private void Awake()
    {
        m_commandQueue = GetComponent<CommandQueue>();
    }

    private void OnEnable()
    {
        m_currentIndex = 0;
    }

    private void OnDisable()
    {
        if (CurrentCommand.IsStarted)
        {
            CurrentCommand.OnStop(gameObject);
        }
    }

    private void Update()
    {
        if (!CurrentCommand.IsStarted)
        {
            var msg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_PROCESSING_COMMAND, CurrentIndex);
            MessageDispatcher.Instance.DispatchMessage(msg);

            CurrentCommand.OnStart(gameObject);
        }

        CommandStatus status = CurrentCommand.OnUpdated(gameObject);

        switch (status)
        {
            case CommandStatus.COMPLETE:
                {
                    CurrentCommand.OnStop(gameObject);

                    var msg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_SUCCESSFULLY_PROCESSED_COMMAND, CurrentIndex);
                    MessageDispatcher.Instance.DispatchMessage(msg);

                    CurrentIndex += 1;

                    break;
                }
            case CommandStatus.FAILURE:
                {
                    CurrentCommand.OnStop(gameObject);

                    var failedMsg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_FAILED_TO_PROCESS_COMMAND, CurrentIndex);
                    MessageDispatcher.Instance.DispatchMessage(failedMsg);

                    var stopMsg = new Message(GameEvents.COMMAND_QUEUE_STOP_PROCESSING);
                    MessageDispatcher.Instance.DispatchMessage(stopMsg);

                    break;
                }
            default:
                {
                    break; // No-op
                }
        }
    }
}