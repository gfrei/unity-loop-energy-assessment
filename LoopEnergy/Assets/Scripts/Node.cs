using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Node : MonoBehaviour
{
    public bool isSource;
    public bool isSink;
    public NodeUI nodeUI;
    public bool canRotate;
    public int totalFaces;
    public List<int> interconnectedFaces;

    public UnityEvent OnNodeRotated;
    public UnityEvent<bool> OnEnergyChanged;


    public bool HasEnergy { get => isSource || hasEnergy; set => hasEnergy = value; }
    private bool hasEnergy;
    
    private int currentRotation;
    private Dictionary<int, NodeFace> connectedFaces;

    private void Awake()
    {
        connectedFaces = new Dictionary<int, NodeFace>();
        nodeUI.Init(this);
    }

    public void Rotate()
    {
        OnNodeRotated.Invoke();

        if (!canRotate)
        {
            return;
        }

        currentRotation++;
        currentRotation %= totalFaces;

        UpdateNode();
    }

    private void UpdateNode(Node requester = null)
    {
        HasEnergy = IsReceivingEnergy(out int localSourceFace);

        foreach (int face in interconnectedFaces)
        {
            int rotatedFace = (face + currentRotation) % totalFaces;
            if (connectedFaces.ContainsKey(rotatedFace) && localSourceFace != rotatedFace && requester != connectedFaces[rotatedFace].node)
            {
                connectedFaces[rotatedFace].node.SetEnergyOnNode(HasEnergy, this);
            }
        }

        OnEnergyChanged.Invoke(HasEnergy);
    }

    private bool IsReceivingEnergy(out int localSourceFace)
    {
        foreach (int face in interconnectedFaces)
        {
            int rotatedFace = (face + currentRotation) % totalFaces;

            if (connectedFaces.ContainsKey(rotatedFace))
            {
                var nodeFace = connectedFaces[rotatedFace];
                if (nodeFace.node.IsNodeFaceEnergized(nodeFace.facePosition))
                {
                    localSourceFace = rotatedFace;
                    return true;
                }
            }
        }

        localSourceFace = -1;

        return false;
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

    public bool IsNodeFaceEnergized(int face)
    {
        if (!HasEnergy)
        {
            return false;
        }

        if (!interconnectedFaces.Contains((face + currentRotation) % totalFaces)) 
        { 
            return false;
        }

        return true;
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
}
