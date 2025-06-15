using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PetController với object pooling
// PetController với object pooling và tối ưu hóa
public class PetController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private ParticleSystem startBullet;
    [SerializeField] private float enemyCheckInterval = 0.1f; // Kiểm tra enemy mỗi 0.1s thay vì mỗi frame
    
    private float nextFireTime = 0f;
    private float nextEnemyCheckTime = 0f;
    private Transform enemy;
    private Transform cachedTransform;
    
    public Transform firePoint; // Vị trí bắn đạn
    public float fireRate = 1f; // Tốc độ bắn
    
    void Start()
    {
        cachedTransform = transform; // Cache transform để tối ưu performance
    }

    void Update()
    {
        // Chỉ tìm enemy theo interval thay vì mỗi frame
        if (Time.time >= nextEnemyCheckTime)
        {
            FindNearestEnemy();
            nextEnemyCheckTime = Time.time + enemyCheckInterval;
        }

        // Nếu có enemy, quay về và bắn
        if (enemy != null)
        {
            RotateTowardsEnemy();
            
            // Bắn đạn liên tục
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    
    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Tìm enemy gần nhất
            enemy = enemies[0].transform;
            float minDistSqr = Vector3.SqrMagnitude(cachedTransform.position - enemy.position);
            
            for (int i = 1; i < enemies.Length; i++)
            {
                float distSqr = Vector3.SqrMagnitude(cachedTransform.position - enemies[i].transform.position);
                if (distSqr < minDistSqr)
                {
                    minDistSqr = distSqr;
                    enemy = enemies[i].transform;
                }
            }
        }
        else
        {
            enemy = null;
        }
    }
    
    private void RotateTowardsEnemy()
    {
        Vector3 direction = enemy.position - cachedTransform.position;
        direction.z = 0;
        
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        // Lật mặt pet khi quay về bên trái hoặc phải
        if (direction.x < 0)
            cachedTransform.localScale = new Vector3(-1, 1, 1); // Lật mặt sang trái
        else
            cachedTransform.localScale = new Vector3(1, 1, 1);  // Quay mặt sang phải
    }
    
    private void Fire()
    {
        // Sử dụng object pooling thay vì Instantiate
        Bullet bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            Vector3 shootDirection = firePoint.up;
            bullet.Initialize(firePoint.position, shootDirection);
            
            if (startBullet != null)
                startBullet.Play(); // Phát hiệu ứng bắn
        }
    }
}