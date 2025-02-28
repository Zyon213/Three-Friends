using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    private CameraController camController;
    private float delayTime = 0.5f;

    private void Start()
    {
        camController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            camController.isGameOver = true;
       //     StartCoroutine(DelayBeforeLoad());
            camController.LoadGameOverPage();
        }
    }

    IEnumerator DelayBeforeLoad()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("GameOverPage", LoadSceneMode.Additive);
    }
}
