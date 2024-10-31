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

        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, moveDirection.normalized, obstacleDistance, obstacleLayers);
        if (hitObstacle.collider != null)
        {
            moveDirection = new Vector2(target.transform.position.x - transform.position.x, 0);
            rb2D.velocity = velocity * -1;
        }
    }
}
