using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] Animator gateAnim;
    [SerializeField] Animator playerAnim;
    private void Awake()
    {
        HandleLoadAnimBeginLevel();
    }
    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(currentIndex + 1);
        else
            Debug.Log("Ban da pha dao game!!!");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void HandleLoadAnimBeginLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            StartCoroutine(LoadAnimationLevel());
    }

    IEnumerator LoadAnimationLevel()
    {
        //gateAnim.SetTrigger("Gate_appear");
        GameEvent.OnDisplayStartGate?.Invoke();
        yield return new WaitForSeconds(1.5f);
        playerAnim.gameObject.SetActive(true);
        playerAnim.SetTrigger("Player_appear");
        yield return new WaitForSeconds(1.25f);
        gateAnim.SetTrigger("Gate_disapear");
    }
}
