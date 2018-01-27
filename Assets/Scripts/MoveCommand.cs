using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName="MOVE_COMMAND", menuName="Create Move Command")]
public class MoveCommand : BaseCommand
{
    [SerializeField]
    private float m_duration;

    [SerializeField]
    private Directions m_direction;

    private float m_elapsedTime;
    private OccupyNode m_occupyNode;

    override public void OnStart(GameObject target)
    {
        base.OnStart(target);

        m_elapsedTime = 0;
        m_occupyNode = target.GetComponent<OccupyNode>();
    }

    override public CommandStatus OnUpdated(GameObject target)
    {
        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime >= m_duration)
        {
            GameObject newObj;
            if (m_occupyNode.Current.GetConnectedInDirection(m_direction, out newObj))
            {
                Node newNode = newObj.GetComponent<Node>();
                if (newNode)
                {
                    m_occupyNode.Current = newNode;
                    return CommandStatus.COMPLETE;
                }
            }

            return CommandStatus.FAILURE;
        }
        else
        {
            return CommandStatus.RUNNING;
        }
    }
}