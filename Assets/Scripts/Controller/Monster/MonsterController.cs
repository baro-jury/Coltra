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

    internal PlayerController target;

    [Header("---------- Movement ----------")]
    public SpriteDirection spriteDirection;
    public LayerMask obstacleLayers;

    [Header("---------- Attack ----------")]
    public List<GameObject> bulletPool;
    public GameObject bullet;
    public int amountBulletsToPool;

    public Transform attackPoint;
    public float attacksPerSec;
    public float minimumDistanceAttack;
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
        target = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        InitForAttack();
    }

    void InitColors()
    {
        var list = GameManager.instance.colorData.colorList;
        spriteRenderer.color = list[Random.Range(0, list.Count)];
    }

    void InitForAttack()
    {
        if (bullet != null)
        {
            Transform pool = GameObject.Find("ObjectPool").transform;
            bulletPool = new List<GameObject>();
            GameObject temp;
            for (int i = 0; i < amountBulletsToPool; i++)
            {
                temp = Instantiate(bullet, pool);
                temp.SetActive(false);
                bulletPool.Add(temp);
            }
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        MonsterMove();
    }

    internal void MonsterMove()
    {
        Vector2 direction = target.transform.position - transform.position;
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

}
