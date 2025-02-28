using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUpDownLift : MonoBehaviour
{
    [SerializeField] GameObject upTrigger;
    [SerializeField] GameObject downTrigger;
    [SerializeField] float speed = 0.01f;

    private float initialLiftPos;
    private float finalLiftPos;
    public bool toUp = false;
    public bool toDown = false;
    public bool isOnElevator = false;

    private void Start()
    {
        //    liftWidth = GetComponent<BoxCollider>().size.z;
        initialLiftPos = transform.position.y;
        finalLiftPos = downTrigger.transform.position.y;
    }

    private void Update()
    {
        TriggerLiftMovement();
    }
    public void TriggerLiftMovement()
    {
        if (isOnElevator)
        {
            if (toUp)
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            else if (toDown)
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);

            if (transform.position.y < initialLiftPos)
                transform.position = new Vector3(transform.position.x, initialLiftPos, transform.position.z);
            else if (transform.position.y > finalLiftPos)
                transform.position = new Vector3(transform.position.x, finalLiftPos, transform.position.z );
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
