using UnityEngine;

public abstract class Condition : ScriptableObject
{
    abstract public bool IsConditionSatisfied();

    virtual public void OnStart()
    {
    }

    virtual public void OnUpdate()
    {
    }

    virtual public void OnStop()
    {
    }
}