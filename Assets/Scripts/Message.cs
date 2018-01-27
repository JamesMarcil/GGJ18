public class Message
{
    public string Type { get; private set; }

    public Message(string type)
    {
        Type = type;
    }
}