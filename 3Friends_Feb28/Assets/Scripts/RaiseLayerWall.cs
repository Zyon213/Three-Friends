using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseLayerWall : MonoBehaviour
{
    [SerializeField] Material[] buttonMats;
    [SerializeField] GameObject wall;
    [SerializeField] string matName;
    private readonly float wallRaiseHieght = 15.0f;
    private readonly float raiseSpeed = 0.1f;
    private float wallInitPosY;
    private CameraController mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
        foreach (Material mat in buttonMats)
        {
            if (mat.name == matName)
                this.GetComponent<MeshRenderer>().material = mat;
        }
        wallInitPosY = wall.transform.position.y;
        this.gameObject.layer =  LayerMask.NameToLayer(matName);
    }

    private void Update()
    {
        if (GetComponent<ButtonController>().isWallRaised && !mainCamera.isGameOver)
        {
            wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y + raiseSpeed, wall.transform.position.z);
            if (wall.transform.position.y >= wallInitPosY + wallRaiseHieght)
                wall.transform.position = new Vector3(wall.transform.position.x, (wallInitPosY + wallRaiseHieght), wall.transform.position.z);
        }
    }
}
