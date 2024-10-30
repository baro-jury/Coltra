using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : Singleton<HomeController>
{
    private List<GameObject> panelObjList = new List<GameObject>();

    [Header("---------- Audio ----------")]
    public AudioClip musicHome;
    public AudioClip clickButtonClip;
    public AudioClip gameStartClip;

    [Header("---------- Button ----------")]
    public Button btnPlay;
    public Button btnSetting;
    public Button btnSoundOff;
    public Button btnSoundOn;
    public Button btnMusicOff;
    public Button btnMusicOn;
    public Button btnJoinGame;
    public Button[] btnsBackToMenu;

    [Header("---------- Panel ----------")]
    public GameObject panelMenu;
    public GameObject panelMap;

    [Header("---------- Tab ----------")]
    public GameObject tabSetting;

    [Header("---------- Text ----------")]
    public Text levelText;

    void Start()
    {
        panelObjList.Add(panelMenu);
        panelObjList.Add(panelMap);

        ShowPanel(panelMenu);

        AudioManager.instance.soundSource.mute = false;
        AudioManager.instance.musicSource.mute = false;
        AudioManager.instance.musicSource.clip = musicHome;
        AudioManager.instance.musicSource.Play();
        btnSoundOff.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        btnMusicOff.gameObject.SetActive(AudioManager.instance.musicSource.mute);
        tabSetting.SetActive(false);

        RemoveButtonListener(btnsBackToMenu);
        RemoveButtonListener(btnPlay, btnSetting, btnSoundOff, btnSoundOn, btnMusicOff, btnMusicOn, btnJoinGame);

        foreach (var backBt in btnsBackToMenu)
        {
            backBt.onClick.AddListener(BackToMenu);
        }
        btnPlay.onClick.AddListener(PlayGame);
        btnSetting.onClick.AddListener(Setting);
        btnSoundOff.onClick.AddListener(TurnOnSound);
        btnSoundOn.onClick.AddListener(TurnOffSound);
        btnMusicOff.onClick.AddListener(TurnOnMusic);
        btnMusicOn.onClick.AddListener(TurnOffMusic);
        btnJoinGame.onClick.AddListener(JoinGame);
    }

    public void ShowPanel(GameObject panelObj)
    {
        foreach (var obj in panelObjList)
        {
            if (obj == panelObj)
            {
                obj.SetActive(true);
            }
            else
            {
                if (obj.activeInHierarchy)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void RemoveButtonListener(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    void PlayGame()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        ShowPanel(panelMap);
    }

    void Setting()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        tabSetting.SetActive(true);
    }


    void TurnOnSound()
    {
        AudioManager.instance.soundSource.mute = false;
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        btnSoundOff.gameObject.SetActive(false);
    }

    void TurnOffSound()
    {
        AudioManager.instance.soundSource.mute = true;
        btnSoundOff.gameObject.SetActive(true);
    }

    void TurnOnMusic()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        AudioManager.instance.musicSource.mute = false;
        btnMusicOff.gameObject.SetActive(false);
    }

    void TurnOffMusic()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        AudioManager.instance.musicSource.mute = true;
        btnMusicOff.gameObject.SetActive(true);
    }

    void BackToMenu()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        ShowPanel(panelMenu);
    }

    void JoinGame()
    {
        AudioManager.instance.soundSource.PlayOneShot(gameStartClip);
        SceneManager.LoadScene("Level 1");
    }
}
