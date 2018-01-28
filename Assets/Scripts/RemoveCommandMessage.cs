public class RemoveCommandMessage : Message
{
    public int Index { get; private set; } 

    public RemoveCommandMessage(int index)
        : base(GameEvents.REMOVE_COMMAND)
    {
        Index = index;
    }
}