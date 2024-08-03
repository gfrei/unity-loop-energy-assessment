using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NodeFace
{
    public Node node;
    public int facePosition;
}

public class NodeConnection : MonoBehaviour
{
    public NodeFace nodeAFace;
    public NodeFace nodeBFace;
}
