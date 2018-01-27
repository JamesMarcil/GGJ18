using UnityEngine;

public abstract class BaseCommand : ScriptableObject
{
    public virtual void OnStart(GameObject target)
    {
    }

    public virtual void OnStop(GameObject target)
    {
    }

    public abstract CommandStatus OnUpdated(GameObject target);
}