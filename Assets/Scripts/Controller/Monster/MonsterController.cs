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
    [SerializeField] private ColorData colorData;

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
        rb2D = GetComponent<Rigidbody2D>();

        InitColors();
        target = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void InitColors()
    {
        colorList = new List<Color>();
        colorList.Add(colorData.grey);
        colorList.Add(colorData.green);
        colorList.Add(colorData.blue);
        colorList.Add(colorData.red);
        colorList.Add(colorData.yellow);

        //colorList.Add(colorData.red);
        //colorList.Add(colorData.orange);
        //colorList.Add(colorData.yellow);
        //colorList.Add(colorData.green);
        //colorList.Add(colorData.blue);
        //colorList.Add(colorData.indigo);
        //colorList.Add(colorData.violet);
    }

    void Update()
    {

    }

    internal virtual void MonsterMove()
    {
        Vector2 direction = target.transform.position - transform.position;
        Vector2 velocity = direction.normalized * monster.velocity;
        rb2D.velocity = velocity;


        Flip(rb2D.velocity.x);

    }

    protected void Flip(float xVel)
    {
        Vector3 scale = transform.localScale;
        if (xVel * scale.x * (int)spriteDirection < 0) scale.x *= -1f;
        transform.localScale = scale;
    }

}
