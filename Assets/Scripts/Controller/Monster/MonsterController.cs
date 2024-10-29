using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum SpriteDirection
    {
        LEFT = -1,
        RIGHT = 1
    }

    [Header("---------- Monster ----------")]
    [SerializeField] private Monster monster;

    internal SpriteRenderer spriteRenderer;
    internal Rigidbody2D rb2D;
    internal Animator anim;
    internal List<Color> colorList;

    [SerializeField] internal PlayerController target;

    [Header("---------- Movement ----------")]
    public SpriteDirection spriteDirection;
    internal Vector2 direction;

    [Header("---------- Attack ----------")]
    public GameObject bulletPrefab;
    public List<GameObject> bulletPool;
    public int amountBulletsToPool;

    public Transform attackPoint;
    public float attacksPerSec;
    public float bulletForce;

    [SerializeField] protected int meleeDamage = 1;
    protected float lastTimeAttack;
    protected bool isAttacking;

    [Header("---------- Stats ----------")]
    [SerializeField] private int currentHP;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();

        InitColors();
        //target = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        direction = new Vector2(target.transform.position.x - transform.position.x, 0);

        InitForAttack();
    }

    void InitColors()
    {
        var list = GameManager.instance.colorData.colorList;
        spriteRenderer.color = list[Random.Range(0, list.Count)];
    }

    void InitForAttack()
    {
        if (bulletPrefab != null)
        {
            Transform pool = GameObject.Find("ObjectPool").transform;
            bulletPool = new List<GameObject>();
            GameObject temp;
            for (int i = 0; i < amountBulletsToPool; i++)
            {
                temp = Instantiate(bulletPrefab, pool);
                temp.GetComponent<EnemyBulletController>()._renderer.color = spriteRenderer.color;
                temp.SetActive(false);
                bulletPool.Add(temp);
            }
        }
    }

    void Update()
    {
        MonsterAttack();
    }

    void FixedUpdate()
    {
        MonsterMove();
    }

    void MonsterMove()
    {
        Vector2 velocity = direction.normalized * monster.velocity;
        rb2D.velocity = velocity;

        Flip(rb2D.velocity.x);
    }

    void Flip(float xVel)
    {
        Vector3 scale = transform.localScale;
        if (xVel * scale.x * (int)spriteDirection < 0) scale.x *= -1f;
        transform.localScale = scale;
    }

    void MonsterAttack()
    {
        if (Time.time < lastTimeAttack + 1 / attacksPerSec)
        {
            return;
        }

        //anim.SetTrigger("IsAttacking");
        RangedAttack();

        lastTimeAttack = Time.time;
    }

    void RangedAttack()
    {
        Vector2 shootDir = target.transform.position - attackPoint.position;
        if (shootDir.x * direction.x <= 0) return;
        ShootBullet(shootDir);
    }

    void ShootBullet(Vector2 direction)
    {
        GameObject bullet = GetPooledBullet();
        var bulletCtrl = bullet.GetComponent<EnemyBulletController>();
        if (bullet != null)
        {
            bullet.transform.position = attackPoint.position;
            float rotateValue = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotateValue + (int)bulletCtrl.spriteDirection);
            bullet.SetActive(true);
            bulletCtrl._rigid.AddForce(direction.normalized * bulletForce);
        }
    }

    private GameObject GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

}
