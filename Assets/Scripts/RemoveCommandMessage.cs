public class RemoveCommandMessage : Message
{
    public int Index { get; private set; } 

    public RemoveCommandMessage(int index)
        : base(GameEvents.COMMAND_QUEUE_REMOVE_COMMAND)
    {
        Index = index;
    }
}