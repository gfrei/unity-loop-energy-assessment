using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private GameObject levelCompletePanel;

    private List<Node> changedNodes = new List<Node>();
    private List<Node> litNodes = new List<Node>();
    private List<Node> sinkNodes = new List<Node>();
    private LevelPrefab levelInstance;
    private AudioController audioController;
    private bool isLevelComplete;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        levelCompletePanel.SetActive(false);
        SetLevel();
    }

    private void SetLevel()
    {
        // Instantiate current level
        levelInstance = Instantiate(gameConfig.currentLevel.prefab);
        levelInstance.Init();

        // Set node connections
        foreach (var connection in levelInstance.nodeConnections) 
        {
            connection.nodeAFace.node.AddConnection(connection);
            connection.nodeBFace.node.AddConnection(connection);
        }

        // Add callback and check sinks
        foreach (var node in levelInstance.nodes)
        {
            node.OnNodeRotated.AddListener(OnNodeRotation);

            if (node.isSink)
            {
                sinkNodes.Add(node);
            }
        }
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
                node.PropagateEnergy(AddToEnergizedList);
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

        // Play rotation clip
        audioController.PlayRotateSfx();
    }

    private void AddToEnergizedList(Node node, bool changedState)
    {
        litNodes.Add(node);

        if (changedState)
        {
            changedNodes.Add(node);
        }
    }

    private void CheckSinks()
    {
        if (isLevelComplete)
        {
            return;
        }

        foreach (var sink in sinkNodes)
        {
            if (!sink.HasEnergy)
            {
                // Not all sinks have energy
                return;
            }
        }

        CompleteLevel();

        OpenLevelCompletePanel();
    }

    public void OpenLevelCompletePanel()
    {
        audioController.PlayCompleteLevelSfx();
        levelCompletePanel.SetActive(true);
    }

    public void ReturnToSelection()
    {
        audioController.PlayButtonSfx();
        SceneManager.LoadScene("SelectionScene");
    }

    public void CompleteLevel()
    {
        isLevelComplete = true;
        audioController.PlayButtonSfx();
        ProgressionController.Instance.CompleteLevel(gameConfig.currentLevel);
    }

}
