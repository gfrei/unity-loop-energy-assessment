using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<Node> nodes;
    [SerializeField] private List<NodeConnection> nodeConnections;
    
    private List<Node> changedNodes = new List<Node>();
    private List<Node> litNodes = new List<Node>();

    private void Start()
    {
        changedNodes = new List<Node>();
        litNodes = new List<Node>();
        SetLevel();
    }

    private void SetLevel()
    {
        foreach (var connection in nodeConnections) 
        {
            connection.nodeAFace.node.AddConnection(connection);
            connection.nodeBFace.node.AddConnection(connection);
        }

        foreach (var node in nodes)
        {
            node.Init(this);
        }
    }

    public void OnNodeRotation()
    {
        litNodes.Clear();
        changedNodes.Clear();

        foreach (var node in nodes)
        {
            if (node.isSource)
            {
                node.PropagateEnergy();
            }
        }

        foreach(var node in nodes)
        {
            if (!litNodes.Contains(node))
            {
                node.HasEnergy = false;
                changedNodes.Add(node);
            }
        }

        foreach (var node in changedNodes)
        {
            node.UpdateState();
        }
    }

    public void AddToEnergizedList(Node node, bool changedState)
    {
        litNodes.Add(node);
        if (changedState)
        {
            changedNodes.Add(node);
        }
    }
}
