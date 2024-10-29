using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject gameOverUI;
    private void Awake()
    {
        GameEvent.OnDeadEvent += ShowGameOverUI;
    }

    private void OnDestroy()
    {
        GameEvent.OnDeadEvent -= ShowGameOverUI;
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }


    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    public void ShowUI()
    {

    }

    public void HideUI()
    {

    }
}
