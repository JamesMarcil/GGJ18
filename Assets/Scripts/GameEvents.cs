public static class GameEvents
{
    public const string COMMAND_FAILED_TO_ENQUEUE = "GameEvents_COMMAND_FAILED_TO_ENQUEUE";
    public const string COMMAND_SUCCESSFULLY_ENQUEUED = "GameEvents_COMMAND_SUCCESSFULLY_ENQUEUED";

    public const string COMMAND_QUEUE_REMOVE_COMMAND = "GameEvents_COMMAND_QUEUE_REMOVE_COMMAND";
    public const string COMMAND_QUEUE_ENQUEUE_COMMAND = "GameEvents_COMMAND_QUEUE_ENQUEUE_COMMAND";
    public const string COMMAND_QUEUE_EMPTY = "GameEvents_COMMAND_QUEUE_EMPTY";
    public const string COMMAND_QUEUE_NO_LONGER_AT_CAPACITY = "GameEvents_COMMAND_QUEUE_NO_LONGER_AT_CAPACITY";
    public const string COMMAND_QUEUE_AT_CAPACITY = "GameEvents_COMMAND_QUEUE_AT_CAPACITY";
    public const string COMMAND_QUEUE_BEGIN_PROCESSING = "GameEvents_COMMAND_QUEUE_BEGIN_PROCESSING";

    public const string GAME_SUCCESS = "GameEvents_GAME_SUCCESS";
    public const string GAME_FAILURE = "GameEvents_GAME_FAILURE";
}