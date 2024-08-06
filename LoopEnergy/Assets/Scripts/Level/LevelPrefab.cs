using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefab : MonoBehaviour
{
    [HideInInspector] public Node[] nodes;
    [HideInInspector] public NodeConnection[] nodeConnections;

    [SerializeField] private GameObject levelObject;

    public GameObject nodesParent;

    public void Init()
    {
        nodes = GetComponentsInChildren<Node>();
        nodeConnections = GetComponentsInChildren<NodeConnection>();
    }
}
