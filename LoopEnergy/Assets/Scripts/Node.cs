using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private bool isSource;
    [SerializeField] private bool isSink;
    [SerializeField] private bool canRotate;
    [SerializeField] private int totalSides;
    [SerializeField] private List<int> interconnectedSides;
    public bool HasEnergy { get => isSource || hasEnergy; set => hasEnergy = value; }
    private bool hasEnergy;
    
    private int currentRotation;
    private Dictionary<int, NodeSide> connectedSides;

    private void Awake()
    {
        connectedSides = new Dictionary<int, NodeSide>();
    }

    public void Rotate()
    {
        if (!canRotate)
        {
            return;
        }

        currentRotation++;
        currentRotation %= totalSides;

        UpdateNodeConnectedSides();
    }

    private void UpdateNodeConnectedSides()
    {
        HasEnergy = IsReceivingEnergy(out int localSourceSide);

        foreach (int side in interconnectedSides)
        {
            int rotatedSide = side + currentRotation;
            if (connectedSides.ContainsKey(rotatedSide) && localSourceSide != rotatedSide)
            {
                connectedSides[rotatedSide].node.SetEnergyOnNode(HasEnergy);
            }
        }
    }

    private bool IsReceivingEnergy(out int localSourceSide)
    {
        foreach (int side in interconnectedSides)
        {
            int rotatedSide = side + currentRotation;

            if (connectedSides.ContainsKey(rotatedSide))
            {
                var nodeSide = connectedSides[rotatedSide];
                if (nodeSide.node.IsNodeSideEnergized(nodeSide.sidePosition))
                {
                    localSourceSide = rotatedSide;
                    return true;
                }
            }
        }

        localSourceSide = -1;

        return false;
    }

    public void SetEnergyOnNode(bool receivingEnergy)
    {
        if (receivingEnergy == HasEnergy)
        {
            return;
        }

        HasEnergy = receivingEnergy;

        UpdateNodeConnectedSides();
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
            connectedSides[connection.sideA.sidePosition] = connection.sideB;
        }
        else
        {
            connectedSides[connection.sideB.sidePosition] = connection.sideA;
        }

    }
}
