using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingLiftController : MonoBehaviour
{
    private float startPos;
    [SerializeField] GameObject travelPos;
    [SerializeField] float speed ;
    [SerializeField] int sign;

    private void Start()
    {
        startPos = transform.position.z;
     //   travelDistance = travelPos.transform.position.z;
    }

    private void Update()
    {
        SwingLift();
    }
    public void SwingLift()
    {
        transform.Translate(Vector3.forward * speed * sign * Time.deltaTime);
        if (transform.position.z <= startPos || transform.position.z >= travelPos.transform.position.z)
            sign = -sign;
        if (transform.position.z < startPos)
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos);
        else if (transform.position.z > travelPos.transform.position.z)
            transform.position = new Vector3(transform.position.x, transform.position.y, travelPos.transform.position.z);

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
