public class CommandMessage : Message
{
    public BaseCommand Command { get; private set; }

    public CommandMessage(string type, BaseCommand command)
        : base(type)
    {
        Command = command;
    }
}