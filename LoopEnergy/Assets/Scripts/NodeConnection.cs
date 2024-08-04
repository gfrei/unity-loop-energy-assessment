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

    public bool IsConnected()
    {
        bool nodeAFacing = nodeAFace.node.IsNodeFaceOpen(nodeAFace.facePosition);
        bool nodeBFacing = nodeBFace.node.IsNodeFaceOpen(nodeBFace.facePosition);

        return nodeAFacing && nodeBFacing;
    }

    public bool ContainsFace(Node node, int facePosition)
    {
        bool nodeA = nodeAFace.node == node && nodeAFace.facePosition == facePosition;
        bool nodeB = nodeBFace.node == node && nodeBFace.facePosition == facePosition;

        return nodeA || nodeB;
    }

    public Node GetConnectedNode(Node node)
    {
        if (nodeAFace.node == node)
        {
            return nodeBFace.node;
        }
        else
        {
            return nodeAFace.node;
        }
    }
}
