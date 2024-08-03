using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnection : MonoBehaviour
{
    [SerializeField] private NodeBase nodeA;
    [SerializeField] private int nodeAConnectPosition;

    [SerializeField] private NodeBase nodeB;
    [SerializeField] private int nodeBConnectPosition;
}
