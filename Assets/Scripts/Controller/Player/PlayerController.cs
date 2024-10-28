using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    

    private float inputX;
    public int player_dir = 1; // Huong cua nhan vat, de tinh toan huong cua vien dan
    private bool facing_right = true;

    private Dictionary<Color, int> absorbedBullets = new();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collid = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        InputControl();
        FlipController();
    }

    private void InputControl()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
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

    private void Flip()
    {
        player_dir *= -1;
        facing_right = !facing_right;
        rb.transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (inputX == 1 && !facing_right)
            Flip();
        else if (inputX == -1 && facing_right)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.oneWayPlatform) && !platformCantFall.Contains(collision.gameObject))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.oneWayPlatform))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(collid, platformCollider);
        yield return new WaitForSeconds(1.0f);
        Physics2D.IgnoreCollision(collid, platformCollider, false);
    }

    private bool IsGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f, groundLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f, groundLayer);

        Gizmos.DrawRay(groundCheck.position, Vector2.down * 0.05f);

        if (hit.collider != null)
        {
            Gizmos.DrawSphere(hit.point, 0.05f);
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
            HandleGameOver();
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
}
