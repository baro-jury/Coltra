using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public enum SpriteDirection
    {
        TOP = 90,
        BOTTOM = -90,
        LEFT = 0,
        RIGHT = 180
    }
    public SpriteDirection spriteDirection;

    [SerializeField] float _lifeTime = 5.0f;
    internal Rigidbody2D _rigid;
    internal SpriteRenderer _renderer;
    public Color bulletColor;

    private void Awake()
    {
        //color = CharacterBase.getCharactorColor();
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //_rigid.velocity = _speed * Time.deltaTime * this.transform.right;
    }

    protected void HideBullet()
    {
        gameObject.SetActive(false);
        _rigid.velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        StartCoroutine(DeactiveAfterTime());
    }

    IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        HideBullet();
    }

    // Xu ly VFX va va cham
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject g = ObjectPooling.Instance.getVFX();
        //g.transform.position = this.transform.position;
        //g.SetActive(true);
        //this.gameObject.SetActive(false);

        //if (collision.CompareTag("Wall"))
        //    Destroy(collision.gameObject);
    }

    public void SetBulletColor(Color color)
    {
        this.bulletColor = color;
    }

    public void ChangeBulletColor()
    {
        if (bulletColor != null)
            GetComponent<SpriteRenderer>().color = bulletColor;
    }
}
