using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SubmitUserName : MonoBehaviour
{
    [SerializeField] TMP_InputField userNameInput;
    [SerializeField] GameObject warningMessage;
//    private readonly string userName = "User";
    private int userNameLength;
    private MainManager mainManager;

    private void Start()
    {
        warningMessage.gameObject.SetActive(false);
        userNameInput.text = null;
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    public void LoadMainMenu()
    {
        mainManager.PlaySFXSound(mainManager.buttonClickClip);
        if (!ValidateUserName())
            warningMessage.gameObject.SetActive(true);
        else
        {
            /*
            if (userNameLength == 0)
            {
                MainManager.Instance.userName = userName;
            }
            else
                */
            MainManager.Instance.userName = userNameInput.text;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        
    }

    public bool ValidateUserName()
    {
        userNameLength = userNameInput.text.Length;
        //   if (userNameLength == 0 || userNameLength >= 4 && userNameLength <= 10)
           if (userNameLength >= 4 && userNameLength <= 10)

        {
            return true;
        }
        return false;
    }

}
