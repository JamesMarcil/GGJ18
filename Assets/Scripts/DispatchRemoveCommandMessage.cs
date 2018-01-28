using UnityEngine;

public class DispatchRemoveCommandMessage : MonoBehaviour
{
    public void RemoveCommand()
    {
        int index = transform.GetSiblingIndex() - 1;

        var msg = new CommandAtIndexMessage(GameEvents.COMMAND_QUEUE_REMOVE_COMMAND, index);
        MessageDispatcher.Instance.DispatchMessage(msg);
    }
}