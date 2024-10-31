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
    public Button btnSound;
    public Button btnMusic;
    public Button btnHome;
    public Button btnHomeOver;

    [Header("---------- Panel ----------")]
    public GameObject panelPause;

    [Header("---------- Tab ----------")]
    public GameObject tabTutorial;

    [Header("---------- Text ----------")]
    public Text levelText;

    private bool soundToggle;
    private bool musicToggle;

    [SerializeField] Sprite checkBg;
    [SerializeField] Sprite unCheckBg;

    void Awake()
    {
        AudioManager.instance.musicSource.clip = musicIngame;
        AudioManager.instance.musicSource.Play();

        //btnSoundOff.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        //btnMusicOff.gameObject.SetActive(AudioManager.instance.musicSource.mute);
        //tabTutorial.SetActive(false);

        //RemoveButtonListener(btnPause, btnResume, btnReplay, btnSoundOff, btnSoundOn, btnMusicOff, btnMusicOn, btnHome);

        soundToggle = !AudioManager.instance.soundSource.mute;
        musicToggle = !AudioManager.instance.musicSource.mute;

        btnSound.GetComponent<Image>().sprite = soundToggle ? checkBg : unCheckBg;
        btnMusic.GetComponent<Image>().sprite = musicToggle ? checkBg : unCheckBg;

        btnPause.onClick.AddListener(Pause);
        btnResume.onClick.AddListener(Resume);
        btnReplay.onClick.AddListener(Replay);
        btnSound.onClick.AddListener(TurnToggleSound);
        btnMusic.onClick.AddListener(TurnToggleMusic);
        btnHome.onClick.AddListener(GoHome);
        btnHomeOver.onClick.AddListener(GoHome);
    }

    private void OnDestroy()
    {
        btnPause.onClick.RemoveAllListeners();
        btnResume.onClick.RemoveAllListeners();
        btnReplay.onClick.RemoveAllListeners();
        btnSound.onClick.RemoveAllListeners();
        btnMusic.onClick.RemoveAllListeners();
        btnHome.onClick.RemoveAllListeners();
        btnHomeOver.onClick.RemoveAllListeners();
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
        Time.timeScale = 1;
    }

    public void GoHome()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        SceneManager.LoadScene("Home");
        Time.timeScale = 1;
    }

    void TurnToggleSound()
    {
        soundToggle = !soundToggle;
        AudioManager.instance.soundSource.mute = !soundToggle;
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        btnSound.GetComponent<Image>().sprite = soundToggle ? checkBg : unCheckBg;
    }
    void TurnToggleMusic()
    {
        musicToggle = !musicToggle;
        AudioManager.instance.musicSource.mute = !musicToggle;
        AudioManager.instance.musicSource.PlayOneShot(clickButtonClip);
        btnMusic.GetComponent<Image>().sprite = musicToggle ? checkBg : unCheckBg;
    }

    //void TurnOnSound()
    //{
    //    AudioManager.instance.soundSource.mute = false;
    //    AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
    //    btnSoundOff.gameObject.SetActive(false);
    //}

    //void TurnOffSound()
    //{
    //    AudioManager.instance.soundSource.mute = true;
    //    btnSoundOff.gameObject.SetActive(true);
    //}

    //void TurnOnMusic()
    //{
    //    AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
    //    AudioManager.instance.musicSource.mute = false;
    //    btnMusicOff.gameObject.SetActive(false);
    //}

    //void TurnOffMusic()
    //{
    //    AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
    //    AudioManager.instance.musicSource.mute = true;
    //    btnMusicOff.gameObject.SetActive(true);
    //}

}
