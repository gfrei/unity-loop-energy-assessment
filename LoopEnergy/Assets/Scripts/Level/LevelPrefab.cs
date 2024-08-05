using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefab : MonoBehaviour
{
    public List<Node> nodes;
    public List<NodeConnection> nodeConnections;

    [SerializeField] private GameObject levelObject;
}
