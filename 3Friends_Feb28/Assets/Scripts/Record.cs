using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Record : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI elapsedTimeText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    private MainManager mainManager;


    private UIController uiController;

    private void Start()
    {
        uiController = GameObject.Find("GameUI").GetComponent<UIController>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

    }
    public void DisplayRecord()
    {
        //  MainManager.Instance.LoadHighScore();

        userNameText.text = MainManager.Instance.userName;
        elapsedTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", uiController.minute, uiController.second, uiController.microSecond);
        bestTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", MainManager.Instance.minute, MainManager.Instance.second, MainManager.Instance.microSecond);
    }
}
