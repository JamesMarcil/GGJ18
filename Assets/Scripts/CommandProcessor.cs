using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    private BaseCommandAsset CurrentCommand
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
            else
            {
                var msg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_PROCESSING_COMMAND, CurrentIndex);
                MessageDispatcher.Instance.DispatchMessage(msg);

                m_numCompleteCommands = 0;

                for (int i = 0; i < m_players.Length; i++)
                {
                    GameObject obj = m_players[i];

                    BaseCommand command = CurrentCommand.Make(obj);

                    command.OnStatusChanged.AddListener(OnStatusChanged);

                    m_commands[i] = command;
                }
            }
        }
    }

    private int m_currentIndex;

    [SerializeField]
    private CommandQueue m_commandQueue;

    private GameObject[] m_players;
    private BaseCommand[] m_commands;
    private int m_numCompleteCommands;

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_BEGIN_PROCESSING, OnBeginProcessing);
    }

    private void Start()
    {
        m_players = GameObject.FindGameObjectsWithTag("Player");
        m_commands = new BaseCommand[m_players.Length];
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_BEGIN_PROCESSING, OnBeginProcessing);
    }

    private void OnBeginProcessing(Message message)
    {
        CurrentIndex = 0;
    }

    private void OnStatusChanged(BaseCommand command)
    {
        m_numCompleteCommands++;

        command.enabled = false;
        command.OnStatusChanged.RemoveListener(OnStatusChanged);

        if (m_numCompleteCommands >= m_commands.Length)
        {
            bool allComplete = true;
            for (int i = 0; i < m_commands.Length; i++)
            {
                BaseCommand cmd = m_commands[i];
                if (cmd.Status == CommandStatus.FAILURE)
                {
                    allComplete = false;
                }
            }

            if (allComplete)
            {
                var msg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_SUCCESSFULLY_PROCESSED_COMMAND, CurrentIndex);
                MessageDispatcher.Instance.DispatchMessage(msg);

                CurrentIndex += 1;
            }
            else
            {
                var failedMsg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_FAILED_TO_PROCESS_COMMAND, CurrentIndex);
                MessageDispatcher.Instance.DispatchMessage(failedMsg);

                var stopMsg = new Message(GameEvents.COMMAND_QUEUE_STOP_PROCESSING);
                MessageDispatcher.Instance.DispatchMessage(stopMsg);
            }
        }
    }
}