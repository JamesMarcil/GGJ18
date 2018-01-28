using UnityEngine;

[CreateAssetMenu(fileName="ENTITY_IN_GOAL", menuName="Make Entity In Goal Condition")]
public class EntityInGoal : Condition
{
    [SerializeField]
    private Node m_targetNode;

    private GameObject[] m_players;

    private void Start()
    {
        m_players = GameObject.FindGameObjectsWithTag("Player");
    }

    override public bool IsConditionSatisfied()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            GameObject obj = m_players[i];

            var component = obj.GetComponent<OccupyNode>();
            if (component.Current == m_targetNode)
            {
                return true;
            }
        }

        return false;
    }
}