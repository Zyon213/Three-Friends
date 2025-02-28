using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownLiftController : MonoBehaviour
{
    private float startPos;
    [SerializeField] GameObject upDistance;
    [SerializeField] int sign = 1;
    [SerializeField] float speed = 0.1f;

    private void Start()
    {
        startPos = transform.position.y;
    }

    private void Update()
    {
        LiftUpDownMovement();
    }
    public void LiftUpDownMovement()
    {
        transform.Translate(Vector3.up * speed * sign * Time.deltaTime);
        if (transform.position.y <= startPos || transform.position.y >= upDistance.transform.position.y)
            sign = -sign;
        if (transform.position.y < startPos)
        {
            transform.position = new Vector3(transform.position.x, startPos, transform.position.z);
        }
        else if (transform.position.y > upDistance.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, upDistance.transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
