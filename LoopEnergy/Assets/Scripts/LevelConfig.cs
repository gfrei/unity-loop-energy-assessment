using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public List<Node> nodes;
    public List<NodeConnection> nodeConnections;
    [HideInInspector] public int totalSinks;

    [SerializeField] private GameObject levelObject;

    private void Start()
    {
        foreach (var node in nodes)
        {
            if (node.isSink)
            {
                totalSinks++;
            }
        }
    }
}
