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

    private const string PROGRESS = "Progress";

    void IsFirstTimePlay()
    {
        if (!PlayerPrefs.HasKey("_IsFirstTimePlay"))
        {
            PlayerPrefs.SetInt(PROGRESS, 1);
            PlayerPrefs.SetInt("_IsFirstTimePlay", 0);
        }
    }

    public ColorData colorData;

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt(PROGRESS, level);
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt(PROGRESS);
    }

}
