using UnityEngine;

public abstract class BaseCommand : ScriptableObject
{
    public bool IsStarted { get; private set; }

    public CommandTypes Type
    {
        get
        {
            return m_type;
        }
    }

    [SerializeField]
    protected CommandTypes m_type;

    public virtual void OnStart(GameObject target)
    {
        IsStarted = true;
    }

    public virtual void OnStop(GameObject target)
    {
        IsStarted = false;
    }

    public abstract CommandStatus OnUpdated(GameObject target);
}