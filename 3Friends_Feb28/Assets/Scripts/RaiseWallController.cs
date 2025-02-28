using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWallController : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject wall;
    private ButtonController buttonController;
    private Animator anim;

    private void Start()
    {
        buttonController = button.GetComponent<ButtonController>();
        anim = wall.GetComponent<Animator>();
    }

    private void Update()
    {
        if (buttonController.isPressed)
        {
            anim.Play("WallRaiseAnim");
        }
    }
}
