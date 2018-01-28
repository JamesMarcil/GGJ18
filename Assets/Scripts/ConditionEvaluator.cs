using UnityEngine;

public class ConditionEvaluator : MonoBehaviour
{
    private Condition[] m_conditions;

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(GameEvents.COMMAND_QUEUE_STOP_PROCESSING, OnStopProcessingCommands);
    }

    private void Start()
    {
        m_conditions = GetComponents<Condition>();
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.COMMAND_QUEUE_STOP_PROCESSING, OnStopProcessingCommands);
    }

    private void OnStopProcessingCommands(Message message)
    {
        bool allConditionsSatisfied = true;
        for (int i = 0; i < m_conditions.Length; i++)
        {
            var condition = m_conditions[i];
            if (!condition.IsConditionSatisfied())
            {
                allConditionsSatisfied = false;
            }
        }

        Message msg;
        if (allConditionsSatisfied)
        {
            msg = new Message(GameEvents.GAME_SUCCESS);
        }
        else
        {
            msg = new Message(GameEvents.GAME_FAILURE);
        }

        MessageDispatcher.Instance.DispatchMessage(msg);
    }
}