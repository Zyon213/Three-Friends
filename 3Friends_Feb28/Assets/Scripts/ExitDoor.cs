using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject exitSign;
    [SerializeField] GameObject exitDoor;
    [SerializeField] string playerName;
    public bool isInside = false;

    private void Update()
    {
        if (isInside)
        {
            exitSign.gameObject.SetActive(true);
        }
        else
            exitSign.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            other.gameObject.GetComponent<PlayerController>().isAtExit = true;
            exitDoor.GetComponent<Animator>().Play("DoorOpen");
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            other.gameObject.GetComponent<PlayerController>().isAtExit = false;
            exitDoor.GetComponent<Animator>().Play("DoorClose");
            isInside = false;
        }
    }
}
    