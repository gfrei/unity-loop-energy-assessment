using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] protected int totalSides;
    [SerializeField] protected List<int> interconnectedSides;
    [SerializeField] protected bool canRotate;
    public bool HasEnergy { get; protected set; }
    
    protected int currentRotation;
    protected Dictionary<int, Node> nodeConnections;


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
