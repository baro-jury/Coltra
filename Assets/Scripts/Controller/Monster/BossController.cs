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

    internal override void MonsterMove()
    {
        Vector2 velocity = moveDirection.normalized * monster.velocity;
        rb2D.velocity = velocity;

        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < obstacleDistance)
        {
            rb2D.velocity = Vector2.zero;
        }

        Flip(rb2D.velocity.x);
    }

    internal override void MonsterState()
    {
        
    }

    protected override void UpdateAnimation()
    {

    }
}
