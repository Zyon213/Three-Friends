using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string userName;
    public int microSecond;
    public int second ;
    public int minute ;

    [Header("-------------Audio Source----------")]
    public AudioSource musicSource;
    public AudioSource carSource;
    public AudioSource SFXSource;

    [Header("-------------Audio Clip-----------")]
    public AudioClip backgroundClip;
    public AudioClip buttonClickClip;
    public AudioClip jumpClip;
    public AudioClip wallCollideClip;
    public AudioClip fallClip;
    public AudioClip enterClip;
    public AudioClip redCarClip;
    public AudioClip blueCarClip;
    public AudioClip yellowCarClip;
    public AudioClip pushButton;
    private CameraController camMain;


    private void Start()
    {
        musicSource.clip = backgroundClip;
        musicSource.Play();
        camMain = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void PlayCarSound(AudioClip clip)
    {
     //   if (!camMain.isGameOver)
        {
            carSource.clip = clip;
            carSource.Play();
        }
    }

    public void StopCarSound()
    {
        carSource.clip = null;
        carSource.Stop();
    }

    public void PlaySFXSound(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    [System.Serializable]
    class HighScore
    {
        public string userName;
        public int microSecond;
        public int second;
        public int minute;
    }

    public void SaveHighScore()
    {
        HighScore hiScore = new HighScore();

        hiScore.userName = userName;
        hiScore.microSecond = microSecond;
        hiScore.second = second;
        hiScore.minute = minute;

        string json = JsonUtility.ToJson(hiScore);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore hiScore = JsonUtility.FromJson<HighScore>(json);
            userName = hiScore.userName;
            microSecond = hiScore.microSecond;
            second = hiScore.second;
            minute = hiScore.minute;
        }
    }
 
}
