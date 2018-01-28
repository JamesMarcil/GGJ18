using UnityEngine;

public abstract class BaseCommandAsset : ScriptableObject
{
    public CommandTypes Type
    {
        get
        {
            return m_type;
        }
    }

    [SerializeField]
    protected CommandTypes m_type;

    public abstract BaseCommand Make(GameObject target);
}