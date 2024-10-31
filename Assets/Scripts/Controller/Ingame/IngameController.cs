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

    [Header("---------- Panel ----------")]
    public GameObject panelPause;
    public GameObject panelBoss;
    public Slider barBossHP;

    [Header("---------- Text ----------")]
    public Text levelText;

    private bool soundToggle = true;
    private bool musicToggle = true;

    void Start()
    {
        //AudioManager.instance.musicSource.clip = musicIngame;
        //AudioManager.instance.musicSource.Play();

        //btnSoundOff.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        //btnMusicOff.gameObject.SetActive(AudioManager.instance.musicSource.mute);

        panelPause.SetActive(false);
        panelBoss.SetActive(false);
        barBossHP.wholeNumbers = true;

        RemoveButtonListener(btnPause, btnResume, btnReplay, btnSound, btnMusic, btnHome);

        btnPause.onClick.AddListener(Pause);
        btnResume.onClick.AddListener(Resume);
        btnReplay.onClick.AddListener(Replay);
        btnSound.onClick.AddListener(TurnToggleSound);
        btnMusic.onClick.AddListener(TurnToggleMusic);
        btnHome.onClick.AddListener(GoHome);
    }

    private void OnDestroy()
    {
        btnPause.onClick.RemoveAllListeners();
        btnResume.onClick.RemoveAllListeners();
        btnReplay.onClick.RemoveAllListeners();
        btnSound.onClick.RemoveAllListeners();
        btnMusic.onClick.RemoveAllListeners();
        btnHome.onClick.RemoveAllListeners();
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
        AudioManager.instance.soundSource.mute = soundToggle;
        GameManager.instance.SetSoundState(soundToggle);
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        btnSound.GetComponent<Image>().color = soundToggle ? Color.white : Color.gray;
    }
    void TurnToggleMusic()
    {
        musicToggle = !musicToggle;
        AudioManager.instance.soundSource.mute = musicToggle;
        GameManager.instance.SetMusicState(musicToggle);
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        btnSound.GetComponent<Image>().color = soundToggle ? Color.white : Color.gray;
    }

    public void SetBossHP(float curHP, float maxHP)
    {
        barBossHP.minValue = 0;
        barBossHP.maxValue = maxHP;
        barBossHP.value = curHP;
    }

}
