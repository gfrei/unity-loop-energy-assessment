using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private RectTransform uiObject;
    [SerializeField] private List<GameObject> faceObjects;

    private bool canRotate;
    private int totalFaces;
    private List<int> interconnectedFaces;

    public void Init(bool canRotate, int totalFaces, List<int> interconnectedFaces)
    {
        this.canRotate = canRotate;
        this.totalFaces = totalFaces;
        this.interconnectedFaces = interconnectedFaces;

        foreach (var face in interconnectedFaces)
        {
            faceObjects[face].SetActive(true);
        }
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

}
