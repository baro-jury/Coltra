using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public Transform shootPoint;
    public GameObject bulletPrefab;

    [SerializeField] private float _shootTime;

    private PlayerController player;
    float _shootTimeCount = 0;


    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
    
    private void Update()
    {
        TestShoot(); // dung de test shoot
    }

    private void TestShoot()
    {
        if (_shootTimeCount > 0)
        {
            _shootTimeCount -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject g = ObjectPool.Instance.GetObject(bulletPrefab);
            g.transform.position = shootPoint.position;

            float dir = player.player_dir;
            g.transform.rotation = Quaternion.Euler(0, (dir == 1 ? 0 : -180), 0);

            g.SetActive(true);

            _shootTimeCount = _shootTime;
        }
    }


    //public void Shoot()
    //{
    //    if (currentColor == null) return;

    //    foreach (var bulletEntry in absorbedBullets)
    //    {
    //        Color bulletColor = bulletEntry.Key;
    //        int bulletCount = bulletEntry.Value;

    //        for (int i = 0; i < bulletCount; i++)
    //        {
    //            SpawnBullet(bulletColor);

    //            //StartCoroutine(ReturnBulletAfterTime(bullet, 2f));
    //        }
    //    }

    //    absorbedBullets.Clear();
    //    currentColor = null;
    //}

    /*
    private void SpawnBullet(Color bulletColor)
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = shootPoint.position;
        bullet.GetComponent<SpriteRenderer>().color = bulletColor;
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * 10f;
    }
    */
}
