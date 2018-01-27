using UnityEngine;

public class DispatchMessage : MonoBehaviour
{
    public void Message(string type)
    {
        var msg = new Message(type);
        MessageDispatcher.Instance.DispatchMessage(msg);
    }
}