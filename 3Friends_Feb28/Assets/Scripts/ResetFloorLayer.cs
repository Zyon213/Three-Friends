using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFloorLayer : MonoBehaviour
{
    private ButtonController buttonController;
    [SerializeField] GameObject button;
    [SerializeField] GameObject floor;
    [SerializeField] Material floorMat;
    [SerializeField] Material buttonMat;
    private CameraController mainCam;

    private readonly string defaultLayer = "Default";

    private void Start()
    {
        buttonController = GetComponent<ButtonController>();
        mainCam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void Update()
    {
        if (mainCam.ActivePlayer() != null && buttonController.isLayerOn)
        {
            ChangeMaterial();
        }
    }


    private void ChangeMaterial()
    {
        button.GetComponent<MeshRenderer>().material = buttonMat;
        floor.GetComponent<MeshRenderer>().material = floorMat;
        floor.layer = LayerMask.NameToLayer(defaultLayer);
    }
}
