using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum SpriteDirection
    {
        TOP = 90,
        BOTTOM = -90,
        LEFT = 0,
        RIGHT = 180
    }
    public SpriteDirection spriteDirection;

    public float maxTimeExist = 10f;

    internal virtual void Awake()
    {

    }

    internal virtual void Start()
    {

    }

    void Update()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(BulletExistence());
    }

    IEnumerator BulletExistence()
    {
        yield return new WaitForSeconds(maxTimeExist);
        HideBullet();
    }

    protected void HideBullet()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    HideBullet();
        //}
    }
}
