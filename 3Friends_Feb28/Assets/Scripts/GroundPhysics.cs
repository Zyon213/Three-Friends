using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPhysics : MonoBehaviour
{
    [SerializeField] Material[] materials;
    [SerializeField] GameObject floor;
    private ButtonController button;
    private CameraController mainCam;
    private readonly string blue = "Blue";
    private readonly string red =  "Red";
    private readonly string yellow = "Yellow";
    private readonly string whiteMat = "White";
    public bool isLayerOn;

    public void Start()
    {
        button = GetComponent<ButtonController>();
        mainCam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        floor.layer = LayerMask.NameToLayer(whiteMat);

        floor.GetComponent<MeshRenderer>().material = materials[3];

    }

    private void Update()
    {
        if (mainCam.ActivePlayer() != null)
        {
            NewButtonMaterial();
        }
    }

    private void NewButtonMaterial()
    {
        if (button.isBlue)
        {
            AssingMaterial(blue);
        }
        else if (button.isRed)
        {
            AssingMaterial(red);
        }
        else if (button.isYellow)
        {
            AssingMaterial(yellow);
         }
    }

    private void AssingMaterial(string matName)
    {

        // change the material of the buttton and the floor
        this.GetComponent<MeshRenderer>().material = ChangeMaterial(matName);
        floor.GetComponent<MeshRenderer>().material = ChangeMaterial(matName);

        // change the layer of the player and the floor
        floor.layer = LayerMask.NameToLayer(matName);
         this.mainCam.ActivePlayer().layer = LayerMask.NameToLayer(matName);
        
    }

    private Material ChangeMaterial(string name)
    {
        foreach (Material buttonMaterial in materials)
        {
            if (buttonMaterial.name == name)
                return buttonMaterial;
        }
        return null;
    }
}
