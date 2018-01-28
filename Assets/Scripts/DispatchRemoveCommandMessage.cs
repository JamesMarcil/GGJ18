using UnityEngine;

public class DispatchRemoveCommandMessage : MonoBehaviour
{
    public void RemoveCommand()
    {
        int index = transform.GetSiblingIndex() - 1;

        var msg = new RemoveCommandMessage(index);
        MessageDispatcher.Instance.DispatchMessage(msg);
    }
}