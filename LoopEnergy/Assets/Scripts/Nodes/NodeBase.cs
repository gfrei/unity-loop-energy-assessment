using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    [SerializeField] protected int sides;
    [SerializeField] protected List<int> conectedSides;
    [SerializeField] protected bool hasEnergy;
    [SerializeField] protected bool canRotate;

    private List<NodeBase> adjacentNodes;

    protected int rotation;

    private void Awake()
    {
        adjacentNodes = new List<NodeBase>();
    }

    public void Rotate()
    {
        if (canRotate)
        {
            rotation++;
            rotation %= sides;
        }
    }
}
