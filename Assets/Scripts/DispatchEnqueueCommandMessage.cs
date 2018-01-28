using UnityEngine;

public class DispatchEnqueueCommandMessage : MonoBehaviour
{
    public void EnqueueCommand(BaseCommandAsset command)
    {
        var message = new CommandMessage(GameEvents.COMMAND_QUEUE_ENQUEUE_COMMAND, command);
        MessageDispatcher.Instance.DispatchMessage(message);
    }
}