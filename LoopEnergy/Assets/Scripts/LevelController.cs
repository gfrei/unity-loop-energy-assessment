using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<Node> nodes;
    [SerializeField] private List<NodeConnection> nodeConnections;


    private void Start()
    {
        SetLevel();
    }

    private void SetLevel()
    {
        foreach (var connection in nodeConnections) 
        {
            connection.sideA.node.AddConnection(connection);
            connection.sideB.node.AddConnection(connection);
        }
    }
}
