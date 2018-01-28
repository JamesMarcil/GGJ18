using UnityEngine;

public class EntityInGoal : Condition
{
    [SerializeField]
    private Node m_targetNode;

    private GameObject[] m_players;

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

    override public void OnStart()
    {
        m_players = GameObject.FindGameObjectsWithTag("Player");
    }
}