using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLeftRightLift : MonoBehaviour
{
    [SerializeField] GameObject leftTrigger;
    [SerializeField] GameObject rightTrigger;
    [SerializeField] float speed = 7.0f;

    private float liftWidth = 6.0f;
    private float initialLiftPos;
    private float finalLiftPos;
    public bool toLeft = false;
    public bool toRight = false;
    public bool isOnElevator = false;

    private void Start()
    {
    //    liftWidth = GetComponent<BoxCollider>().size.z;
        initialLiftPos = transform.position.z;
        finalLiftPos = rightTrigger.transform.position.z - liftWidth;
    }

    private void Update()
    {
        TriggerLiftMovement();
    }
    public void TriggerLiftMovement()
    {
        if (isOnElevator)
        {
            if (toLeft)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            else if (toRight)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);

            if (transform.position.z < initialLiftPos)
                transform.position = new Vector3(transform.position.x, transform.position.y, initialLiftPos);
            else if (transform.position.z > finalLiftPos)
                transform.position = new Vector3(transform.position.x, transform.position.y, finalLiftPos);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnElevator = true;
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnElevator = false;
            collision.gameObject.transform.parent = null;
        }
    }
}
