using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;

    private List<Node> changedNodes = new List<Node>();
    private List<Node> litNodes = new List<Node>();
    private LevelConfig levelInstance;

    private void Start()
    {
        changedNodes = new List<Node>();
        litNodes = new List<Node>();
        SetLevel();
    }

    private void SetLevel()
    {
        levelInstance = Instantiate(gameConfig.Levels[0]);

        foreach (var connection in levelInstance.nodeConnections) 
        {
            connection.nodeAFace.node.AddConnection(connection);
            connection.nodeBFace.node.AddConnection(connection);
        }

        foreach (var node in levelInstance.nodes)
        {
            node.Init(this);
        }
    }

    public void OnNodeRotation()
    {
        litNodes.Clear();
        changedNodes.Clear();

        foreach (var node in levelInstance.nodes)
        {
            if (node.isSource)
            {
                node.PropagateEnergy();
            }
        }

        foreach(var node in levelInstance.nodes)
        {
            if (!litNodes.Contains(node))
            {
                node.ClearEnergy();
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
