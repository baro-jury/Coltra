using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public float delaySpawnTime;
    public PlayerController player;
    private float timer;
    private bool isSpawned;
    private GameObject boss;

    void Start()
    {
        timer = 0;

        boss = Instantiate(bossPrefab, transform);
        var bossCtrl = boss.GetComponent<BossController>();
        bossCtrl.target = player;
        boss.SetActive(false);
    }

    void Update()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        if (isSpawned) return;

        timer += Time.deltaTime;
        if (timer > delaySpawnTime)
        {
            boss.SetActive(true);
            isSpawned = true;
        }
    }
}
