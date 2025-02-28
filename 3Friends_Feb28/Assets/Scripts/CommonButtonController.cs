using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonButtonController : MonoBehaviour
{
    private Vector3 initialPos;
    private float pressDistance = 0.5f;
    private float yPos;
    private readonly string blue = "Claire";
    private readonly string red = "John";
    private readonly string yellow = "Thomas";
    public bool isBlue;
    public bool isRed;
    public bool isYellow;
    public bool isPressed;
    public bool isReleased = true;
    public bool isStart = true;
    public bool isOnButton;
//    private float waitSecond = 1.0f;
    private MainManager mainManager;
    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        initialPos = transform.position;
        yPos = transform.position.y - pressDistance;
    }

    public void CheckButton()
    {
        if (isOnButton)
        {
            isPressed = true;
            isReleased = false;
        }
        else
        {
            isReleased = true;
            isPressed = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            isPressed = true;
            isStart = false;
            if (collision.gameObject.name == red)
            {
                isRed = true;
            }
            else if (collision.gameObject.name == yellow)
            {
                isYellow = true;
            }
            else if (collision.gameObject.name == blue)
            {
                isBlue = true;
            }
            isReleased = false;
            mainManager.PlaySFXSound(mainManager.pushButton);

        }
    }
    // Collision detection
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
            transform.position = initialPos;
            isPressed = false;
            isReleased = true;

        }
    }
}
