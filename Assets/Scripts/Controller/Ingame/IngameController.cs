using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameController : Singleton<IngameController>
{
    [Header("---------- Audio ----------")]
    public AudioClip musicIngame;
    public AudioClip clickButtonClip;

    [Header("---------- Button ----------")]
    public Button btnPause;
    public Button btnResume;
    public Button btnReplay;
    public Button btnSoundOff;
    public Button btnSoundOn;
    public Button btnMusicOff;
    public Button btnMusicOn;
    public Button btnHome;

    [Header("---------- Panel ----------")]
    public GameObject panelPause;

    [Header("---------- Tab ----------")]
    public GameObject tabTutorial;

    [Header("---------- Text ----------")]
    public Text levelText;

    void Start()
    {
        AudioManager.instance.musicSource.clip = musicIngame;
        AudioManager.instance.musicSource.Play();

        //btnSoundOff.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        //btnMusicOff.gameObject.SetActive(AudioManager.instance.musicSource.mute);
        //tabTutorial.SetActive(false);

        //RemoveButtonListener(btnPause, btnResume, btnReplay, btnSoundOff, btnSoundOn, btnMusicOff, btnMusicOn, btnHome);

        //btnPause.onClick.AddListener(Pause);
        //btnResume.onClick.AddListener(Resume);
        //btnReplay.onClick.AddListener(Replay);
        //btnSoundOff.onClick.AddListener(TurnOnSound);
        //btnSoundOn.onClick.AddListener(TurnOffSound);
        //btnMusicOff.onClick.AddListener(TurnOnMusic);
        //btnMusicOn.onClick.AddListener(TurnOffMusic);
        //btnHome.onClick.AddListener(GoHome);
    }

    private void RemoveButtonListener(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void Pause()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        Time.timeScale = 0;
        panelPause.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        panelPause.gameObject.SetActive(false);
    }

    public void Replay()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        SceneManager.LoadScene("Home");
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

}
