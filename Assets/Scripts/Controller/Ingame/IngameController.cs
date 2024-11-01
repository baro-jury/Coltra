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
    public AudioClip displayGateClip;
    public AudioClip displayDoneObjectiveClip;

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
    public GameObject panelBoss;
    public Slider barBossHP;

    [Header("---------- Text ----------")]
    public Text levelText;

    private bool soundToggle;
    private bool musicToggle;

    [SerializeField] Sprite checkBg;
    [SerializeField] Sprite unCheckBg;

    protected override void Awake()
    {
        base.Awake();
        GameEvent.OnDisplayStartGate += PlayGateSound;
        GameEvent.OnCompleteLevel += PlayWinSound;
    }

    void Start()
    {
        AudioManager.instance.musicSource.clip = musicIngame;
        AudioManager.instance.musicSource.Play();

        //btnSoundOff.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        //btnMusicOff.gameObject.SetActive(AudioManager.instance.musicSource.mute);

        panelPause.SetActive(false);
        panelBoss.SetActive(false);
        barBossHP.wholeNumbers = true;

        RemoveButtonListener(btnPause, btnResume, btnReplay, btnSound, btnMusic, btnHome);

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
        RemoveButtonListener(btnPause, btnResume, btnReplay, btnSound, btnMusic, btnHome, btnHomeOver);

        GameEvent.OnDisplayStartGate -= PlayGateSound;
        GameEvent.OnCompleteLevel -= PlayWinSound;
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
        GameManager.instance.SetSoundState(!soundToggle);
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        btnSound.GetComponent<Image>().sprite = soundToggle ? checkBg : unCheckBg;
    }
    
    void TurnToggleMusic()
    {
        musicToggle = !musicToggle;
        AudioManager.instance.soundSource.mute = !musicToggle;
        GameManager.instance.SetMusicState(!musicToggle);
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        btnMusic.GetComponent<Image>().sprite = musicToggle ? checkBg : unCheckBg;
    }

    public void SetBossHP(float curHP, float maxHP)
    {
        barBossHP.minValue = 0;
        barBossHP.maxValue = maxHP;
        barBossHP.value = curHP;
    }

    public void PlayGateSound()
    {
        AudioManager.instance.soundSource.PlayOneShot(displayGateClip);
    }

    public void PlayWinSound()
    {
        AudioManager.instance.soundSource.PlayOneShot(displayDoneObjectiveClip);
    }
}
