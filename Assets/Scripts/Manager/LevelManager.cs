using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] LevelDataSO levelData;

    private int currentLevel;
    private List<EnemyData> currentEnemyData;

    private void Awake()
    {
        GameEvent.OnEnemyKill += EnemyKilled;
    }

    private void OnDestroy()
    {
        GameEvent.OnEnemyKill -= EnemyKilled;
    }

    private void Start()
    {
        currentLevel = SceneController.Instance.GetCurrentSceneIndex();
        currentEnemyData = levelData.GetEnemyDataByLevel(currentLevel);
    }

    public void EnemyKilled(CharacterColor color)
    {
        EnemyData enemyData = currentEnemyData.Find(data => data.characterColor == color);
        if (enemyData != null)
        {
            enemyData.quanity--;

            CheckMissionProgress();
        }
    }

    private void CheckMissionProgress()
    {
        foreach (var enemyData in currentEnemyData)
        {
            if (enemyData.quanity > 0)
            {
                return;
            }
        }

        CompleteLevel();
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Completed!");
    }

}
