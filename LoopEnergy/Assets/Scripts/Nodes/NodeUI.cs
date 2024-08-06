using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private Vector3 rotationStep;
    private int rotationCount;

    public void Init(Node node)
    {
        canRotate = node.canRotate;
        totalFaces = node.totalFaces;
        interconnectedFaces = node.interconnectedFaces;
        isSinkObject.gameObject.SetActive(node.isSink);
        isSourceObject.gameObject.SetActive(node.isSource);
        rotationStep = new Vector3(0f, 0f, -360f / totalFaces);


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
        Vector3 fakeRotation = rotationStep * 0.2f;
        

        if (canRotate)
        {
            // Animation example with code
            uiObject.transform.DORotate(newRotation, 0.15f).SetEase(Ease.OutBack);
        }
        else
        {
            // Animation example with code
            uiObject.transform.DORotate(fakeRotation, 0.07f).SetEase(Ease.InOutFlash)
                .OnComplete(() => uiObject.transform.DORotate(Vector3.zero, 0.07f).SetEase(Ease.InOutFlash));
        }
    }

    public void OnEnergyChanged(bool hasEnergy)
    {
        hasEnergyObject.gameObject.SetActive(hasEnergy);
        noEnergyObject.gameObject.SetActive(!hasEnergy);
    }

}
