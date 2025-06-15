using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script để bắn đạn (ví dụ cho Player hoặc Weapon)
public class WeaponController : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform firePoint;
    public float fireRate = 0.5f;
    
    private float nextFireTime;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Lấy bullet từ pool thay vì Instantiate
        Bullet bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            Vector3 shootDirection = firePoint.up;
            bullet.Initialize(firePoint.position, shootDirection);
        }
    }
}
