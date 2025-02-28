using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSidePlayer : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    private Animator anim;
    private CameraController camController;
    private MainManager mainManager;
    private bool isEnterClip;
    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        anim = GetComponent<Animator>();
        camController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void Update()
    {
        if (camController.CheckExit())
        {
            ParentPlayersToLift();
            anim.Play("PushSideMove");

            if (!isEnterClip)
            {
                mainManager.PlaySFXSound(mainManager.enterClip);
                isEnterClip = true;

            }
        }
    }

    private void ParentPlayersToLift()
    {
        foreach (GameObject player in players)
        {
            player.gameObject.transform.parent = this.transform;
        }
    }
}
