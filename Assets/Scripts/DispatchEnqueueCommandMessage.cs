using UnityEngine;

public class DispatchEnqueueCommandMessage : MonoBehaviour
{
    public void EnqueueCommand(BaseCommand command)
    {
        var message = new EnqueueCommandMessage(command);
        MessageDispatcher.Instance.DispatchMessage(message);
    }
}