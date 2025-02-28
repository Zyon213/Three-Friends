using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string playerName;
    [SerializeField] Transform destPostion;
    private float delay = 1.5f;
    private bool isTeleported;
    private GameObject player;

    private void Update()
    {
        if (isTeleported)
            TeleportPlayer();
    }

   private void TeleportPlayer()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, destPostion.position.z);
        isTeleported = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(playerName);
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == playerName)
        {
            StartCoroutine(TeleportDelay(collision));
            player = collision.gameObject;
        }
    }

    IEnumerator TeleportDelay( Collision collision)
    {
        yield return new WaitForSeconds(delay);
        isTeleported = true;
   //     collision.gameObject.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, destPostion.position.z);
    }
}
