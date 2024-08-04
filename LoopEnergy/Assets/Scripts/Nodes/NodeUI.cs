using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private RectTransform uiObject;
    [SerializeField] private RectTransform hasEnergyObject;
    [SerializeField] private RectTransform noEnergyObject;
    [SerializeField] private RectTransform isSourceObject;
    [SerializeField] private RectTransform isSinkObject;
    [SerializeField] private List<GameObject> faceObjects;

    private bool canRotate;
    private int totalFaces;
    private List<int> interconnectedFaces;

    public void Init(Node node)
    {
        canRotate = node.canRotate;
        totalFaces = node.totalFaces;
        interconnectedFaces = node.interconnectedFaces;
        isSinkObject.gameObject.SetActive(node.isSink);
        isSourceObject.gameObject.SetActive(node.isSource);

        foreach (var face in interconnectedFaces)
        {
            faceObjects[face].SetActive(true);
        }

        OnEnergyChanged(node.HasEnergy);
    }

    public void OnRotateNode()
    {
        float rotationStep = 360f / totalFaces;
        Vector3 rotation = new Vector3(0f, 0f, -rotationStep);

        if (canRotate)
        {
            uiObject.Rotate(rotation);
        }
        else
        {
            //Run cant rotate feedback
        }
    }

    public void OnEnergyChanged(bool hasEnergy)
    {
        hasEnergyObject.gameObject.SetActive(hasEnergy);
        noEnergyObject.gameObject.SetActive(!hasEnergy);
    }

}
