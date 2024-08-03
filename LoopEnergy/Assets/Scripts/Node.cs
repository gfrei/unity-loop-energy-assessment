using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isSource;
    public bool isSink;

    [SerializeField] private NodeUI nodeUI;
    [SerializeField] private bool canRotate;
    [SerializeField] private int totalFaces;
    [SerializeField] private List<int> interconnectedFaces;
    public bool HasEnergy { get => isSource || hasEnergy; set => hasEnergy = value; }
    private bool hasEnergy;
    
    private int currentRotation;
    private Dictionary<int, NodeFace> connectedFaces;

    private void Awake()
    {
        connectedFaces = new Dictionary<int, NodeFace>();
        nodeUI.Init(canRotate, totalFaces, interconnectedFaces);
    }

    public void Rotate()
    {
        if (!canRotate)
        {
            return;
        }

        currentRotation++;
        currentRotation %= totalFaces;

        UpdateNode();
    }

    private void UpdateNode()
    {
        HasEnergy = IsReceivingEnergy(out int localSourceFace);

        foreach (int face in interconnectedFaces)
        {
            int rotatedFace = face + currentRotation;
            if (connectedFaces.ContainsKey(rotatedFace) && localSourceFace != rotatedFace)
            {
                connectedFaces[rotatedFace].node.SetEnergyOnNode(HasEnergy);
            }
        }
    }

    private bool IsReceivingEnergy(out int localSourceFace)
    {
        foreach (int face in interconnectedFaces)
        {
            int rotatedFace = face + currentRotation;

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

    public void SetEnergyOnNode(bool receivingEnergy)
    {
        if (receivingEnergy == HasEnergy)
        {
            return;
        }

        HasEnergy = receivingEnergy;

        UpdateNode();
    }

    public bool IsNodeFaceEnergized(int face)
    {
        if (!HasEnergy)
        {
            return false;
        }

        if (!interconnectedFaces.Contains(face + currentRotation)) 
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
