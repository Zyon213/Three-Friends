using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class CameraController : MonoBehaviour
{
    [Header("Serializedfields")]
    [SerializeField] GameObject gameOverPage;
    [SerializeField] GameObject winPage;
    [SerializeField] string[] stages;
    [SerializeField] TextMeshProUGUI levelText;

    [Header("Public readonly variables")]
    public Vector3 offset;

    [Header("Public variables")]
    public GameObject[] players;
    public bool isGameOver;
    public int level = 0;

    [Header("Private readonly variables")]
    private readonly float followHeight = -5.0f;
    private readonly string winGamePage = "WinPage";
    private readonly string yellow = "Thomas";
    private readonly string blue = "Claire";
    private readonly string red = "John";
    private readonly float reloadDelay = 3.0f;
    private readonly float camOffset = 5.0f;
    private readonly float delaySound = 2.0f;

    [Header("Private variables")]
    private GameObject activePlayer = null;
    private GameObject player;
    private string currentStageName;
    private int stageSize;
    private int index = 0;
    private UIController uiController;
    private Record record;
    private CountDown counter;
    private MainManager mainManager;
    private bool isKeyPressed;

   void Start()
   {
        isGameOver = false;
        gameOverPage.gameObject.SetActive(false);
        winPage.gameObject.SetActive(false);
        record = GameObject.Find("GameUI").GetComponent<Record>();
        uiController = GameObject.Find("GameUI").GetComponent<UIController>();
        currentStageName = SceneManager.GetActiveScene().name;
        stageSize = stages.Length;
        counter = GameObject.Find("GameUI").GetComponent<CountDown>();
        offset = this.transform.position;
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

   // Update is called once per frame
   void LateUpdate()
   {
       this.player = ActivePlayer();
       if (player != null && counter.counter < 0)
       {
           if (player.transform.position.y >= followHeight )
           {
                if (player.GetComponent<PlayerController>().yPos != this.player.transform.position.y)
                    transform.position = new Vector3(transform.position.x, player.transform.position.y + camOffset, player.transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x,transform.position.y, player.transform.position.z);
            }
        }
   }

   private void Update()
   {
        this.player = ActivePlayer();
        if (CheckEntered())
        {
            this.player = null;
            LoadWinPage();
        }
        DisplayLevel();

        PlayCarEngine();

    }


    private void PlayCarEngine()
    {
        if (player != null && player.GetComponent<PlayerController>().horizontalInput != 0 && !isGameOver )
        {
            if (player.name == yellow)
            {
                mainManager.PlayCarSound(mainManager.yellowCarClip);
            }
            if (player.name == blue)
            {
                mainManager.PlayCarSound(mainManager.blueCarClip);
            }
            if (player.name == red)
            {
                mainManager.PlayCarSound(mainManager.redCarClip);
            }
        }
        else if (isGameOver)
        {
            mainManager.StopCarSound();
        }
    }


   public GameObject ActivePlayer()
   {
       foreach (GameObject currentPlayer in players)
       {
           if (currentPlayer.gameObject.GetComponent<PlayerController>().isActive)
           {
               activePlayer = currentPlayer;
               return activePlayer;
           }
       }
       return null;
   }


   public bool CheckExit()
   {
       foreach (GameObject player in players)
       {
           if (!player.GetComponent<PlayerController>().isAtExit)
           {
               return false;
           }
       }
       return true;
   }

    private bool CheckEntered()
    {
        foreach (GameObject player in players)
        {
            if (!player.GetComponent<PlayerController>().isEntered)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator DelayWinPageReload()
    {
        yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene(winGamePage, LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    IEnumerator DelayEngineSound()
    {
        yield return new WaitForSeconds(delaySound);
        isKeyPressed = false;
    }
    public void LoadWinPage()
    {
       if (CheckEntered())
            uiController.GameHighScore();
        record.DisplayRecord();
        winPage.gameObject.SetActive(true);
        Time.timeScale = 0;
        counter.counter = 3;
    }

    public void LoadGameOverPage()
    {
        //    StartCoroutine(DelayBeforeReload(reloadDelay));
        gameOverPage.gameObject.SetActive(true);
        Time.timeScale = 0;
        counter.counter = 3;
    }

    // load the next level from stages string array chosing by name
    public void LoadNextGame()
    {
        int stageLength = 0;
        stageLength = stages.Length;
        for (int i = 0; i < stageLength; i++)
        {
            if (currentStageName == stages[i])
            {
                if ((i + 1) == stageLength)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else
                    SceneManager.LoadScene(stages[i + 1]);
            }
        }

    }
    public void DisplayLevel()
    {
        foreach (string stageName in stages)
        {
            if (currentStageName == stageName)
            {
                level = index + 1;
            }
            index++;
        }
        if (index >= stageSize)
            index = 0;
        levelText.text = level.ToString();

    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
