using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

public class Node : MonoBehaviour
{
    [Header("Node Config")]
    public bool isSource;
    public bool isSink;
    public bool canRotate;
    public int totalFaces;
    public List<int> interconnectedFaces;
    [SerializeField] private NodeUI nodeUI;

    [Header("Events")]
    public UnityEvent OnNodeRotated;
    public UnityEvent<bool> OnEnergyChanged;

    public bool HasEnergy { get => isSource || hasEnergy; set => hasEnergy = value; }
    private bool hasEnergy;
    
    private LevelController levelController;
    private List<Node> connectedNodes;
    private List<NodeConnection> nodeConnections;

    private void Awake()
    {
        connectedNodes = new List<Node>();
        nodeConnections = new List<NodeConnection>();
        nodeUI.Init(this);
    }

    public void Init(LevelController levelController)
    {
        this.levelController = levelController;
        UpdateCurrentConnections();
    }

    public void AddConnection(NodeConnection connection)
    {
        nodeConnections.Add(connection);
    }
    
    public void PropagateEnergy(Node source = null)
    {
        levelController.AddToEnergizedList(this, !HasEnergy);

        HasEnergy = true;

        UpdateCurrentConnections();

        foreach (var node in connectedNodes)
        {
            if (node != source)
            {
                node.PropagateEnergy(this);
            }
        }
    }

    private void UpdateCurrentConnections()
    {
        connectedNodes.Clear();

        foreach (var connection in nodeConnections)
        {
            if (connection.IsConnected())
            {
                connectedNodes.Add(connection.GetConnectedNode(this));
            }
        }
    }

    public void UpdateState()
    {
        OnEnergyChanged.Invoke(HasEnergy);
    }

    public bool IsNodeFaceOpen(int face)
    {
        return interconnectedFaces.Contains(face);
    }

    public void Rotate()
    {
        OnNodeRotated.Invoke();

        if (!canRotate)
        {
            return;
        }

        for (int i = 0; i < interconnectedFaces.Count; i++)
        {
            interconnectedFaces[i]++;
            interconnectedFaces[i] %= totalFaces;
        }

        levelController.OnNodeRotation();
    }
}
