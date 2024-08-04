using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Node : MonoBehaviour
{
    [Header("Node Config")]
    public bool isSource;
    public bool isSink;
    public bool canRotate;
    public int totalFaces;
    public List<int> interconnectedFaces;
    public NodeUI nodeUI;

    [Header("Events")]
    public UnityEvent OnNodeRotated;
    public UnityEvent<bool> OnEnergyChanged;

    public bool HasEnergy { get => isSource || hasEnergy; set => hasEnergy = value; }
    private bool hasEnergy;
    
    private Dictionary<int, NodeFace> connectedFaces;

    private void Awake()
    {
        connectedFaces = new Dictionary<int, NodeFace>();
        nodeUI.Init(this);
    }

    public void AddConnection(NodeConnection connection)
    {
        if (connection.nodeAFace.node == this)
        {
            connectedFaces[connection.nodeAFace.facePosition] = connection.nodeBFace;
        }
        else
        {
            connectedFaces[connection.nodeBFace.facePosition] = connection.nodeAFace;
        }
    }

    public void SetEnergyOnNode(bool receivingEnergy, Node requester)
    {
        if (receivingEnergy == HasEnergy)
        {
            return;
        }

        HasEnergy = receivingEnergy;

        UpdateNode(requester);
    }

    public bool IsNodeFaceEnergized(int face) => HasEnergy && interconnectedFaces.Contains(face);

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

        UpdateNode();
    }

    private void UpdateNode(Node requester = null)
    {
        HasEnergy = IsReceivingEnergy(out int localSourceFace);

        foreach (int face in interconnectedFaces)
        {
            if (connectedFaces.ContainsKey(face) && localSourceFace != face && requester != connectedFaces[face].node)
            {
                connectedFaces[face].node.SetEnergyOnNode(HasEnergy, this);
            }
        }

        OnEnergyChanged.Invoke(HasEnergy);
    }

    private bool IsReceivingEnergy(out int localSourceFace)
    {
        foreach (int face in interconnectedFaces)
        {
            if (connectedFaces.ContainsKey(face))
            {
                var nodeFace = connectedFaces[face];
                if (nodeFace.node.IsNodeFaceEnergized(nodeFace.facePosition))
                {
                    localSourceFace = face;
                    return true;
                }
            }
        }

        localSourceFace = -1;

        return false;
    }
}
