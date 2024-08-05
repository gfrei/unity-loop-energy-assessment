using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;

    private List<Node> changedNodes = new List<Node>();
    private List<Node> litNodes = new List<Node>();
    private List<Node> sinkNodes = new List<Node>();
    private LevelPrefab levelInstance;

    private void Start()
    {
        SetLevel();
    }

    private void SetLevel()
    {
        levelInstance = Instantiate(gameConfig.currentLevel.prefab);

        foreach (var connection in levelInstance.nodeConnections) 
        {
            connection.nodeAFace.node.AddConnection(connection);
            connection.nodeBFace.node.AddConnection(connection);
        }

        foreach (var node in levelInstance.nodes)
        {
            node.Init(this);
            if (node.isSink)
            {
                sinkNodes.Add(node);
            }
        }
    }

    private void CheckSinks()
    {
        foreach(var sink in sinkNodes)
        {
            if (!sink.HasEnergy)
            {
                return;
            }
        }

        CompleteLevel();
        ReturnToSelection();
    }


    public void OnNodeRotation()
    {
        litNodes.Clear();
        changedNodes.Clear();

        // Propagate energy from source and save it to litNodes
        foreach (var node in levelInstance.nodes)
        {
            if (node.isSource)
            {
                node.PropagateEnergy();
            }
        }

        // Remove energy from every node outside litNodes
        foreach(var node in levelInstance.nodes)
        {
            if (!litNodes.Contains(node))
            {
                node.ClearEnergy();
                changedNodes.Add(node);
            }
        }

        // Update changed nodes
        foreach (var node in changedNodes)
        {
            node.UpdateState();
        }

        // Check Sinks for level completion
        CheckSinks();
    }

    public void AddToEnergizedList(Node node, bool changedState)
    {
        litNodes.Add(node);

        if (changedState)
        {
            changedNodes.Add(node);
        }
    }

    public void ReturnToSelection()
    {
        SceneManager.LoadScene("SelectionScene");
    }

    public void CompleteLevel()
    {
        ProgressionController.Instance.CompleteLevel(gameConfig.currentLevel);
    }

}
