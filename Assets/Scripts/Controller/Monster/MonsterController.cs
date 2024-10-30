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
    public LayerMask obstacleLayers;
    public float obstacleDistance;
    internal Vector2 moveDirection;

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

    [Header("---------- State ----------")]
    public float liveTime;
    private float timer;

    public CharacterColor bulletColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
        rb2D = GetComponent<Rigidbody2D>();

        InitColors();
        //target = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        moveDirection = new Vector2(target.transform.position.x - transform.position.x, 0);
        timer = 0;

        InitForAttack();

    }

    void InitColors()
    {
        //characterColor = ColorData.GetRandomColor();
        //Color color = ColorData.GetColor(characterColor);
        //spriteRenderer.color = color;

        //var list = GameManager.instance.colorData.colorList;
        //spriteRenderer.color = list[Random.Range(0, list.Count)];
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
                //temp.GetComponent<EnemyBulletController>()._renderer.color = spriteRenderer.color;
                temp.GetComponent<EnemyBulletController>()._renderer.color = ColorData.GetColor(bulletColor);
                temp.SetActive(false);
                bulletPool.Add(temp);
            }
        }
    }

    void Update()
    {
        MonsterAttack();
        MonsterState();
    }

    void FixedUpdate()
    {
        MonsterMove();
    }

    void MonsterMove()
    {
        Vector2 velocity = moveDirection.normalized * monster.velocity;
        rb2D.velocity = velocity;
        
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, moveDirection.normalized, obstacleDistance, obstacleLayers);
        if (hitObstacle.collider != null)
        {
            moveDirection = new Vector2(target.transform.position.x - transform.position.x, 0);
            rb2D.velocity = velocity * -1;
        }

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
        if (shootDir.x * moveDirection.x <= 0) return;
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

            if (spriteRenderer.color != null)
                bulletCtrl.SetBulletColor(bulletColor);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.playerBullet))
        {
            //DecreaseHealth();
            if (collision.GetComponent<BulletBase>().bulletColor == bulletColor)
                DecreaseHealth();
            if (IsDead())
            {
                gameObject.SetActive(false);
                GameEvent.OnEnemyKill?.Invoke(bulletColor);
            }

            collision.gameObject.SetActive(false);
        }
    }

    private void DecreaseHealth()
    {
        monster.health--;
    }

    private bool IsDead()
    {
        return monster.health == 0;
    }

    void MonsterState()
    {
        timer += Time.deltaTime;
        if (timer > liveTime)
        {
            Destroy(gameObject);
        }
    }
}
