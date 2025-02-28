using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideElevator : MonoBehaviour
{
    [SerializeField] GameObject hideLift;
    [SerializeField] GameObject showLift;
    [SerializeField] string playerName;


    private void Start()
    {
    
        hideLift.gameObject.SetActive(true);
        showLift.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == playerName)
        {
            hideLift.gameObject.SetActive(false);
            showLift.gameObject.SetActive(true);
        }
    }

}
