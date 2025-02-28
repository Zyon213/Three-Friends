using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountDown : MonoBehaviour
{
    [SerializeField] GameObject countDown;
    [SerializeField] TextMeshProUGUI number;

    private CameraController camController;

   // private UIController uiController;
    private float timeStart = 0.0f;
    private float timeGap = 0.75f;
    public int counter = 3;
    // Start is called before the first frame update
    void Start()
    {
        countDown.gameObject.SetActive(true);
        camController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    //    uiController = GameObject.Find("MainUI").GetComponent<UIController>();
    }
    // Update is called once per frame
    void Update()
    {
        timeStart += Time.unscaledDeltaTime;
        if (timeStart >= timeGap && !camController.isGameOver)
        {
            counter -= 1;
            timeStart = 0;
        //    isCounterOn = false;
        }
        DisplayCounter();

        if (counter < 0)
        {
            countDown.gameObject.SetActive(false);
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void DisplayCounter()
    {
        number.text = counter.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
