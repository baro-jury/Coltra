using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void MakeSingleInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Awake()
    {
        MakeSingleInstance();
        IsFirstTimePlay();
    }

    private const string FIRST_TIME_PLAY = "IsFirstTimePlay";
    private const string PROGRESS = "Progress";

    void IsFirstTimePlay()
    {
        if (!PlayerPrefs.HasKey(FIRST_TIME_PLAY))
        {
            PlayerPrefs.SetInt(FIRST_TIME_PLAY, 0);
            PlayerPrefs.SetInt(PROGRESS, 1);
            PlayerPrefs.Save();
        }
    }

    public ColorData colorData;

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt(PROGRESS, level);
        PlayerPrefs.Save();
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt(PROGRESS);
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteKey(PROGRESS);
        PlayerPrefs.DeleteKey(FIRST_TIME_PLAY);
        PlayerPrefs.Save();

        Application.Quit();
    }

}
