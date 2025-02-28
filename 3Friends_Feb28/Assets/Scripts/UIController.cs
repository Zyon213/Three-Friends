using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] TextMeshProUGUI timeCounterText;
    [SerializeField] GameObject resumePage;
    private string menuPage = "MainMenu";
    public int microSecond ;
    public int second ;
    public int minute ;
    public bool isPauseOn = false;

    private float startTime = 0.0f;
    private readonly float gapTime = 0.01f;
    private readonly int changeTime = 60;

    private CountDown countDown;
    private MainManager mainManager;
    private CameraController camMain;

    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        countDown = GameObject.Find("GameUI").GetComponent<CountDown>();
        resumePage.gameObject.SetActive(false);
        camMain = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    private void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= gapTime)
        {
            microSecond++;
            if (microSecond == changeTime)
            {
                second++;
                microSecond = 0;
                if (second == changeTime)
                {
                    minute++;
                    second = 0;
                }
            }
            startTime = 0;
        }
        TimerCounter();
        DisplayHighScore();
    }

    public void SetHighScore()
    {
        MainManager.Instance.microSecond = microSecond;
        MainManager.Instance.second = second;
        MainManager.Instance.minute = minute;
        MainManager.Instance.SaveHighScore();
    }
    public void GameHighScore()
    {
        
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            if (minute < MainManager.Instance.minute)
            {
                SetHighScore();
            }
            else if (minute == MainManager.Instance.minute)
            {
                if (second < MainManager.Instance.second)
                {
                    SetHighScore();
                }
                else if (second == MainManager.Instance.second)
                {
                    if (microSecond < MainManager.Instance.microSecond)
                        SetHighScore();
                }
            }
        }
        else
            MainManager.Instance.SaveHighScore();
        
    }
    private void TimerCounter()
    {
        timeCounterText.text = string.Format("{0:00}:{1:00}:{2:00}", minute, second, microSecond);

    }
    public void PauseGame()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        if (countDown.counter < 0)
        {
            Time.timeScale = 0;
            countDown.counter = 3;
            isPauseOn = true;
            resumePage.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        Time.timeScale = 1;
        countDown.counter = -1;
        isPauseOn = false;
        resumePage.gameObject.SetActive(false);
    }
    
    public void RestartGame()
    {
        camMain.isGameOver = false;
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void MenuPage()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        SceneManager.LoadScene(menuPage, LoadSceneMode.Single);
    }

    public void DisplayHighScore()
    {
        hiScoreText.text = string.Format("{0:00}:{1:00}:{2:00}", MainManager.Instance.minute, MainManager.Instance.second, MainManager.Instance.microSecond);
    }
}
