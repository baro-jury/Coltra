using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    protected override void InitForAttack()
    {
        if (bulletPrefab != null)
        {
            Transform pool = GameObject.Find("ObjectPool").transform;
            bulletPool = new List<GameObject>();
            GameObject temp;
            for (int i = 0; i < amountBulletsToPool; i++)
            {
                temp = Instantiate(bulletPrefab, pool);
                temp.GetComponent<EnemyBulletController>()._renderer.color = ColorData.GetColor(ColorData.GetRandomColor());
                temp.SetActive(false);
                bulletPool.Add(temp);
            }
        }
    }
}
