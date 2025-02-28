using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPos;
    private float counter = 0.0f;
    private float resetCounter = 2.0f;


    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= resetCounter)
        {
            Instantiate(bulletPrefab, shootPos.position, bulletPrefab.transform.rotation);
            counter = 0.0f;
        }
    }
}
