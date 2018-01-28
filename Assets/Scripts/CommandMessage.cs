public class CommandMessage : Message
{
    public BaseCommandAsset Command { get; private set; }

    public CommandMessage(string type, BaseCommandAsset command)
        : base(type)
    {
        Command = command;
    }
}