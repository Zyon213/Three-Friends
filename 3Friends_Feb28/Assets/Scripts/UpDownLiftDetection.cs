using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownLiftDetection : MonoBehaviour
{
    private TriggerUpDownLift upDownLift;

    [SerializeField] GameObject toUpArrow;
    [SerializeField] GameObject toDownArrow;

    private void Start()
    {
        upDownLift = GameObject.Find("UpDownTriggerLift").GetComponent<TriggerUpDownLift>();
        toUpArrow.gameObject.SetActive(false);
        toDownArrow.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.name == "UpLiftTrigger")
            {
                upDownLift.toUp = true;
                upDownLift.toDown = false;
                toUpArrow.gameObject.SetActive(true);
                toDownArrow.gameObject.SetActive(false);
            }
            if (this.name == "DownLiftTrigger")
            {
                upDownLift.toUp = false;
                upDownLift.toDown = true;
                toUpArrow.gameObject.SetActive(false);
                toDownArrow.gameObject.SetActive(true);
            }
        }
    }
}
