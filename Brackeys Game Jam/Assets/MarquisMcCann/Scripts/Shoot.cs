using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Shooting")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    //public float distToShoot = 6f;
    //public float bulletsPerClip = 5f;
    //float bulletCount;

    [Header("Delays")]
    float delayShoot;
    public float delayShootTime;
    float delayReload;
    public float delayReloadTime;

    // Start is called before the first frame update
    void Start()
    {
        delayReload = delayReloadTime;
        delayShoot = delayShootTime;
    }

    // Update is called once per frame
    void Update()
    {
        delayShoot -= Time.deltaTime;

        if (delayShoot <= 0)
        {
            delayShoot = delayShootTime;
            Shooting();
            
        }
    }

    void Shooting()
    {
            //bulletCount++;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // spawn bullet
            Destroy(bullet, 3f);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); // to access the rigidbody 2d of the bullet
            bulletRB.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // when the bullet shoots add force to the bullet
            // shooting feedback under

    }
}
