using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] float _speed = 100.0f;
    [SerializeField] float _lifeTime = 5.0f;
    Rigidbody2D _rigid;
    public Color? color;
    
    private void Awake()
    {
        //color = CharacterBase.getCharactorColor();
    }
    void Start()
    {
        _rigid = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(DeactiveAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        _rigid.velocity = _speed * Time.deltaTime * this.transform.right;
    }

    IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        _rigid.gameObject.SetActive(false);
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
