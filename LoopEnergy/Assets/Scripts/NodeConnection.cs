using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NodeSide
{
    public Node node;
    public int sidePosition;
}

public class NodeConnection : MonoBehaviour
{
    public NodeSide sideA;
    public NodeSide sideB;
}
