using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject lift;
    private ButtonController buttonController;

    private void Start()
    {
        buttonController = button.GetComponent<ButtonController>();
        floor.gameObject.SetActive(false);
        lift.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (buttonController.isPressed && buttonController.isBlue)
        {
            floor.gameObject.SetActive(true);
            lift.gameObject.SetActive(false);
        }
    }
}
