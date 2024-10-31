using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    [Header("---------- Stats ----------")]
    [SerializeField] private int maxHP;

    protected override void InitStats()
    {
        maxHP = monster.health;
        IngameController.Instance.panelBoss.SetActive(true);
        IngameController.Instance.SetBossHP(monster.health, maxHP);
    }

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

    protected override void DecreaseHealth()
    {
        base.DecreaseHealth();
        IngameController.Instance.SetBossHP(monster.health, maxHP);
    }

    internal override void MonsterState()
    {
        
    }

    protected override void UpdateAnimation()
    {

    }

    protected override void TriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.playerBullet))
        {
            DecreaseHealth();
            if (IsDead())
            {
                IngameController.Instance.panelBoss.SetActive(false);
                gameObject.SetActive(false);
                GameEvent.OnEnemyKill?.Invoke(bulletColor);
            }

            collision.gameObject.SetActive(false);
        }
    }
}
