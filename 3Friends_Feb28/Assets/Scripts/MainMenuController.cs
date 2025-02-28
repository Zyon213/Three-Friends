using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject optionPage;
    [SerializeField] GameObject movementPage;
    [SerializeField] GameObject resetPage;
    private MainManager mainManager;


    private string stage01 = "Stage01";

    private void Start()
    {
        optionPage.gameObject.SetActive(false);
        movementPage.gameObject.SetActive(false);
        resetPage.gameObject.SetActive(false);
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

    }
    public void StatrGame()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        SceneManager.LoadScene(stage01, LoadSceneMode.Single);
    }

    public void OptionPage()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        optionPage.gameObject.SetActive(true);
    }

    public void OpenMovementPage()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        movementPage.gameObject.SetActive(true);
    }

    public void CloseMovementpage()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        movementPage.gameObject.SetActive(false);
    }
    public void CloseOptionPage()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        optionPage.gameObject.SetActive(false);
    }


    public void ResetGame()
    {
        // set variables to default values
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        resetPage.gameObject.SetActive(true);
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            MainManager.Instance.microSecond = 60;
            MainManager.Instance.second = 60;
            MainManager.Instance.minute = 60;
            MainManager.Instance.SaveHighScore();
        }
    }

    public void CloseResetPage()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        resetPage.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        if (MainManager.Instance != null)
            MainManager.Instance.SaveHighScore();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
