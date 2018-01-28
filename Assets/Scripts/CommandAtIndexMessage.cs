public class CommandAtIndexMessage : Message
{
    public int Index { get; private set; } 

    public CommandAtIndexMessage(string type, int index)
        : base(type)
    {
        Index = index;
    }
}