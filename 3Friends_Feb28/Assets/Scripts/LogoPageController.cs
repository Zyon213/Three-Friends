using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoPageController : MonoBehaviour
{
    private readonly string loginPage = "LoginPage";
    [SerializeField] float delay = 4.0f;

    private void Start()
    {
        StartCoroutine(DelayBeroreLoad());
    }
    IEnumerator DelayBeroreLoad()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(loginPage, LoadSceneMode.Additive);
    }

}
