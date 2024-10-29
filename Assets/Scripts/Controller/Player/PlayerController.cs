using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    private float inputX;

    [Header("Shoot")]
    public Transform shootPoint;
    public GameObject bulletPrefab;
    [SerializeField] private float _shootTime;
    float _shootTimeCount = 0;

    [Header("Bullet available")]
    private Dictionary<Color, int> absorbedBullets = new();

    public bool isGray = true;
    public Dictionary<Color, int> ammoCount = new();

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        InputControl();
        FlipController();
        TestShoot();
    }

    private void InputControl()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = inputX * speed * Time.fixedDeltaTime;
        rb.velocity = velocity;
    }

    private void Jump()
    {
        if(IsGround())
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
    }

    private void FlipController()
    {
        if (inputX == 1 && !facing_right)
            Flip();
        else if (inputX == -1 && facing_right)
            Flip();
    }

    private void TestShoot()
    {
        if (_shootTimeCount > 0)
        {
            _shootTimeCount -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g = ObjectPool.Instance.GetObject(bulletPrefab);
            g.transform.position = shootPoint.position;

            float dir = character_dir;
            g.transform.rotation = Quaternion.Euler(0, (dir == 1 ? 0 : -180), 0);

            BulletBase bulletBase = g.GetComponent<BulletBase>();
            bulletBase.SetBulletColor(currentColor);
            bulletBase.ChangeBulletColor();

            g.SetActive(true);

            _shootTimeCount = _shootTime;
        }
    }

    public void AbsorbBullet(Color bulletColor)
    {
        if (currentColor == null)
        {
            currentColor = bulletColor;
        }
        else if (currentColor != bulletColor)
        {
            HandleGameOver(); // Ham nay xu ly trong GameManager, nhung tam thoi de o day
            return;
        }

        if (absorbedBullets.ContainsKey(bulletColor))
        {
            absorbedBullets[bulletColor]++;
        }
        else
        {
            absorbedBullets[bulletColor] = 1;
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over! You absorbed a different color.");
    }

    ///

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.enemyBullet))
        {
            TakeDamage(collision.GetComponent<BulletBase>().bulletColor);
            collision.gameObject.SetActive(false);
        }
    }

    void TakeDamage(Color bulletColor)
    {
        if (isGray)
        {
            AbsorbBullett(bulletColor);
        }
        else if (bulletColor != currentColor)
        {
            DecreaseHealth();
            if (lives == 0)
            {
                Die();
                return;
            }
            currentColor = bulletColor;
            UpdatePlayerColor();
        }
        else
        {
            ammoCount[bulletColor]++;
        }
    }
    
    private void Die()
    {
        Debug.Log("You are dead!!!");
    }

    void AbsorbBullett(Color bulletColor)
    {
        isGray = false;
        currentColor = bulletColor;
        ammoCount[bulletColor] = 1;
        UpdatePlayerColor();
    }

    void UpdatePlayerColor()
    {
        // Update sprite or material color
        GetComponent<SpriteRenderer>().color = currentColor;
    }

    void FireBullet()
    {
        if (!isGray && ammoCount[currentColor] > 0)
        {
            ammoCount[currentColor]--;
            if (ammoCount[currentColor] <= 0)
            {
                currentColor = Color.gray;
                isGray = true;
                UpdatePlayerColor();
            }
        }
    }
}
