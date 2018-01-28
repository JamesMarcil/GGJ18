public static class GameEvents
{
    public const string ENABLE_START_BUTTON = "GameEvents_ENABLE_START_BUTTON";
    public const string DISABLE_START_BUTTON = "GameEvents_DISABLE_START_BUTTON";

    public const string ENQUEUE_COMMAND = "GameEvents_ENQUEUE_COMMAND";

    public const string COMMAND_FAILED_TO_ENQUEUE = "GameEvents_COMMAND_FAILED_TO_ENQUEUE";
    public const string COMMAND_SUCCESSFULLY_ENQUEUED = "GameEvents_COMMAND_SUCCESSFULLY_ENQUEUED";

    public const string COMMAND_QUEUE_NO_LONGER_AT_CAPACITY = "GameEvents_COMMAND_QUEUE_NO_LONGER_AT_CAPACITY";
    public const string COMMAND_QUEUE_AT_CAPACITY = "GameEvents_COMMAND_QUEUE_AT_CAPACITY";

    public const string START_PROCESSING_COMMANDS = "GameEvents_START_PROCESSING_COMMANDS";

    public const string GAME_SUCCESS = "GameEvents_GAME_SUCCESS";
    public const string GAME_FAILURE = "GameEvents_GAME_FAILURE";
}