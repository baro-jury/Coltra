using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletController
{
    internal SpriteRenderer spriteRenderer;

    internal override void Awake()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
