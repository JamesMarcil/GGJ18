using UnityEngine;

public abstract class BaseCommand : ScriptableObject
{
    public virtual void OnStart()
    {
    }

    public virtual void OnStop()
    {
    }

    public abstract CommandStatus OnUpdated();
}