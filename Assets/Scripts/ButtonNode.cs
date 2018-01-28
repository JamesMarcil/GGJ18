using System;
using System.Collections.Generic;

using UnityEngine;

public class ButtonNode : MonoBehaviour
{
    private Node m_node;

    [SerializeField]
    private List<NodeToReplace> m_nodesToReplace = new List<NodeToReplace>();

    private void Start()
    {
        m_node = GetComponent<Node>();
        m_node.OnEnterNode.AddListener(OnEnterNode);
        m_node.OnExitNode.AddListener(OnExitNode);
    }

    private void OnDestroy()
    {
        m_node.OnEnterNode.RemoveListener(OnEnterNode);
        m_node.OnExitNode.RemoveListener(OnExitNode);
    }

    private void SwapNodes()
    {
        for (int i = 0; i < m_nodesToReplace.Count; i++)
        {
            NodeToReplace nodeToReplace = m_nodesToReplace[i];

            Node previousNode = nodeToReplace.Node;
            TileType previousType = previousNode.Type;

            var component = previousNode.gameObject.GetComponent<ReplaceNode>();

            TileType currentType = nodeToReplace.Type;
            Node currentNode = component.ReplaceWithType(currentType);

            nodeToReplace.Node = currentNode;
            nodeToReplace.Type = previousType;
        }

    }

    private void OnEnterNode(GameObject target)
    {
        SwapNodes();
    }

    private void OnExitNode(GameObject target)
    {
        SwapNodes();
    }

    [Serializable]
    public class NodeToReplace
    {
        public Node Node;
        public TileType Type;
    }
}