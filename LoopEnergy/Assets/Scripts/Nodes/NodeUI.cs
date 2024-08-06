using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
This component deals with visual events of each Node
It can have animations, timelines, audio, etc
I used DOTween to make animations as an example
*/
public class NodeUI : MonoBehaviour
{
    [SerializeField] private RectTransform uiObject;
    [SerializeField] private RectTransform hasEnergyObject;
    [SerializeField] private RectTransform noEnergyObject;
    [SerializeField] private RectTransform isSourceObject;
    [SerializeField] private RectTransform isSinkObject;
    [SerializeField] private List<GameObject> faceObjects;

    private bool isSink;
    private bool canRotate;
    private int totalFaces;
    private List<int> interconnectedFaces;
    private Vector3 rotationStep;
    private Vector3 failedRotation;
    private int rotationCount;

    public void Init(Node node)
    {
        isSink = node.isSink;
        canRotate = node.canRotate;
        totalFaces = node.totalFaces;
        interconnectedFaces = node.interconnectedFaces;

        rotationStep = new Vector3(0f, 0f, -360f / totalFaces);
        failedRotation = rotationStep * 0.2f;

        isSinkObject.gameObject.SetActive(node.isSink);
        isSourceObject.gameObject.SetActive(node.isSource);

        foreach (var faceObject in faceObjects)
        {
            faceObject.SetActive(false);
        }

        foreach (int face in interconnectedFaces)
        {
            faceObjects[face].SetActive(true);
        }

        OnEnergyChanged(node.HasEnergy);
    }

    public void OnRotateNode()
    {
        rotationCount++;
        Vector3 newRotation = rotationStep * rotationCount;

        if (canRotate)
        {
            // Animation example with code
            uiObject.transform.DORotate(newRotation, 0.15f).SetEase(Ease.OutBack);
        }
        else
        {
            // Animation example with code
            uiObject.transform.DORotate(failedRotation, 0.07f).SetEase(Ease.InOutFlash)
                .OnComplete(() => uiObject.transform.DORotate(Vector3.zero, 0.07f).SetEase(Ease.InOutFlash));
        }
    }

    public void OnEnergyChanged(bool hasEnergy)
    {
        hasEnergyObject.gameObject.SetActive(hasEnergy);
        noEnergyObject.gameObject.SetActive(!hasEnergy);

        // Animation example with code
        if (hasEnergy && isSink)
        {
            transform.DOShakePosition(0.5f, strength: 10f);
        }
    }

}
