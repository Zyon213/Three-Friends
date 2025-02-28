using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUpDown : MonoBehaviour
{
    [SerializeField] GameObject buttonFront;
    [SerializeField] GameObject wall;
    private ButtonController frontButtonController;
    private Animator anim;

    private MainManager mainManager;
    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        frontButtonController = buttonFront.GetComponent<ButtonController>();
    
        anim = wall.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!frontButtonController.isPressed && frontButtonController.isStart && frontButtonController.isReleased)
        {

            anim.Play("WallIdleAnim");
        }
        else if (frontButtonController.isPressed && !frontButtonController.isReleased)
        {
            anim.Play("WallRaiseAnim");
        }
        else if (!frontButtonController.isPressed  && frontButtonController.isReleased)
        { 
            anim.Play("WallDownAnim");
        }
    }
}
