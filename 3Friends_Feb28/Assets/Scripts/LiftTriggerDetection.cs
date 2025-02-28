using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTriggerDetection : MonoBehaviour
{
    private TriggerLeftRightLift sideLift;
    [SerializeField] GameObject toLeftArrow;
    [SerializeField] GameObject toRightArrow;


    private void Start()
    {
        sideLift = GameObject.Find("LeftRightTriggerLift").GetComponent<TriggerLeftRightLift>();
        toLeftArrow.gameObject.SetActive(false);
        toRightArrow.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (this.name == "LeftLiftTrigger" )
            {
                sideLift.toRight = true;
                sideLift.toLeft = false;
                toRightArrow.gameObject.SetActive(true);
                toLeftArrow.gameObject.SetActive(false);
            }
            if (this.name == "RightLiftTrigger" )
            {
                sideLift.toRight = false;
                sideLift.toLeft = true;
                toRightArrow.gameObject.SetActive(false);
                toLeftArrow.gameObject.SetActive(true);
            }
        }
    }
}
