using UnityEngine;

public class EnqueueCommandMessage : Message
{
    public BaseCommand Command { get; private set; }

    public EnqueueCommandMessage(BaseCommand command)
        : base(GameEvents.ENQUEUE_COMMAND)
        {
            Command = command;
        }
}