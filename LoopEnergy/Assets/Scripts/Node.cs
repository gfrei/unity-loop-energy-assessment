using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private bool isSource;
    [SerializeField] private bool isSink;
    [SerializeField] private int totalSides;
    [SerializeField] private List<int> interconnectedSides;
    [SerializeField] private bool canRotate;
    public bool HasEnergy { get => isSource || hasEnergy; set => hasEnergy = value; }
    private bool hasEnergy;
    
    private int currentRotation;
    private Dictionary<int, Node> nodeConnections;


    private void Awake()
    {
        nodeConnections = new Dictionary<int, Node>();
    }

    public void Rotate()
    {
        if (canRotate)
        {
            currentRotation++;
            currentRotation %= totalSides;
        }
    }

    public bool IsNodeSideEnergized(int side)
    {
        if (!HasEnergy)
        {
            return false;
        }

        if (!interconnectedSides.Contains(side + currentRotation)) 
        { 
            return false;
        }

        return true;
    }

    public void AddConnection(NodeConnection connection)
    {
        if (connection.sideA.node == this)
        {
            nodeConnections[connection.sideA.sidePosition] = connection.sideB.node;
        }
        else
        {
            nodeConnections[connection.sideB.sidePosition] = connection.sideA.node;
        }

    }
}
