using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rigidBd;
    private float zPos;
    private float shootDistance = 15.0f;
    private CameraController mainCam;

    private void Start()
    {
        zPos = transform.position.z;
        rigidBd = GetComponent<Rigidbody>();
        mainCam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    private void Update()
    {
        //     rigidBd.AddForce(Vector3.back * speed);

        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z - speed));
        if (this.transform.position.z <= (zPos - shootDistance))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCam.LoadGameOverPage();
            mainCam.isGameOver = true;
        }
    }
}
