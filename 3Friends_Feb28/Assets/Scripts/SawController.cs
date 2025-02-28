using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    private MainManager mainManager;
    private CameraController mainCam;
    [SerializeField] GameObject sawBlade;
    [SerializeField] GameObject sawHandle;

    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        mainCam = GameObject.Find("Main Camera").GetComponent<CameraController>();

    }
    
    private void Update()
    {
        if (!mainCam.isGameOver)
        {
            sawBlade.GetComponent<Animator>().Play("SawRotate");
            sawHandle.gameObject.GetComponent<Animator>().Play("SawSwing");
        }
        else if (mainCam.isGameOver)
        {
            sawBlade.GetComponent<Animator>().Play("SawBladeIdle");
            sawHandle.gameObject.GetComponent<Animator>().Play("SawHandleIdle");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCam.LoadGameOverPage();
            mainCam.isGameOver = true;
        }
    }
}
