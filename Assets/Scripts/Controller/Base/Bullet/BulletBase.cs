using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] internal float _speed = 100.0f;
    [SerializeField] float _lifeTime = 5.0f;
    internal Rigidbody2D _rigid;
    public Color? color;
    
    private void Awake()
    {
        //color = CharacterBase.getCharactorColor();
    }

    void Start()
    {
        _rigid = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigid.velocity = _speed * Time.deltaTime * this.transform.right;
    }

    protected void HideBullet()
    {
        gameObject.SetActive(false);
        //_rigid.velocity = Vector2.zero;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject g = ObjectPooling.Instance.getVFX();
        //g.transform.position = this.transform.position;
        //g.SetActive(true);
        //this.gameObject.SetActive(false);

        //if (collision.CompareTag("Wall"))
        //    Destroy(collision.gameObject);
    }
}
