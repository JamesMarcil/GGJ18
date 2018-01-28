using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "MOVE_COMMAND", menuName = "Create Move Command")]
public class MoveCommandAsset : BaseCommandAsset
{
    [SerializeField]
    private float m_duration;

    [SerializeField]
    private Directions m_direction;

    override public BaseCommand Make(GameObject target)
    {
        return MoveCommand.Make(target, this);
    }

    private class MoveCommand : BaseCommand
    {
        private MoveCommandAsset m_asset;

        private float m_elapsedTime;
        private OccupyNode m_occupyNode;

        private void Start()
        {
            m_elapsedTime = 0;
            m_occupyNode = GetComponent<OccupyNode>();
        }

        private void Update()
        {
            m_elapsedTime += Time.deltaTime;

            if (m_elapsedTime >= m_asset.m_duration)
            {
                GameObject newObj;
                if (m_occupyNode.Current.GetConnectedInDirection(m_asset.m_direction, out newObj))
                {
                    Node newNode = newObj.GetComponent<Node>();
                    if (newNode)
                    {
                        m_occupyNode.Current = newNode;
                        Status = CommandStatus.COMPLETE;
                    }
                }
                else
                {

                    Status = CommandStatus.FAILURE;
                }
            }
        }

        public static MoveCommand Make(GameObject target, MoveCommandAsset asset)
        {
            var component = target.AddComponent<MoveCommand>();
            component.m_asset = asset;
            return component;
        }
    }

}