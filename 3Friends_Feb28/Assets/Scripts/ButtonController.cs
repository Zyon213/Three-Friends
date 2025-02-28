using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] string buttonName = "ButtonMain";
    [SerializeField] string buttonType;
    private readonly string wallRaise = "WallRaise";
    private readonly string layerChange = "LayerChange";
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
    private MainManager mainManager;
    public bool isLayerOn;
    public bool isWallRaised;
    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        initialPos = transform.position;
        yPos = transform.position.y - pressDistance;
        isLayerOn = false;
        isWallRaised = false;
    }


    private void Update()
    {
        CheckButton();
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

    private void PressButton( Collision collision)
    {
        isPressed = true;
        isStart = false;
        if (collision.gameObject.name == red)
        {
            isRed = true;
            isYellow = false;
            isBlue = false;

        }
        else if (collision.gameObject.name == yellow)
        {
            isRed = false;
            isYellow = true;
            isBlue = false;
        }
        else if (collision.gameObject.name == blue)
        {
            isRed = false;
            isYellow = false;
            isBlue = true;
        }
        isReleased = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == playerName)
        {

            PressButton(collision);
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.name == buttonName)
        {
            PressButton(collision);
        }
        if (buttonType == wallRaise)
        {
            isWallRaised = true;
        }
        else if (buttonType == layerChange)
        {
            isLayerOn = true;
        }
        collision.gameObject.transform.parent = this.transform;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        mainManager.PlaySFXSound(mainManager.pushButton);
        isOnButton = true;

    }
// Collision detection
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == playerName || collision.gameObject.CompareTag("Player"))
        {
            isPressed = false;
            isReleased = true;   
        }
        collision.gameObject.transform.parent = null;
        transform.position = initialPos;
        isOnButton = false;
    }
}
