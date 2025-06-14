using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private PlayerController playerController; // Tham chiếu đến PlayerController
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private ParticleSystem startBullet;
    private float nextFireTime = 0f;
    private Transform enemy;
    public GameObject bulletPrefab; // Prefab của viên đạn
    public Transform firePoint; // Vị trí bắn đạn
    public float fireRate = 1f; // Tốc độ bắn
    void Start()
    {
        // Tìm kẻ địch trong scene
        // enemy = GameObject.FindGameObjectWithTag("Enemy").transform; // Giả sử kẻ địch có tag là "Enemy"
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Tìm enemy gần nhất
            enemy = enemies[0].transform;
            float minDist = Vector3.Distance(transform.position, enemy.position);
            foreach (var e in enemies)
            {
                float dist = Vector3.Distance(transform.position, e.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    enemy = e.transform;
                }
            }
            // Quay về enemy gần nhất
            Vector3 direction = enemy.position - transform.position;
            direction.z = 0;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            // Lật mặt pet khi quay về bên trái hoặc phải
            if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); // Lật mặt sang trái
            else
                transform.localScale = new Vector3(1, 1, 1);  // Quay mặt sang phải

            // Bắn đạn liên tục
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    private void Fire()
    {
        // Tạo viên đạn tại điểm bắn
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        startBullet.Play(); // Phát hiệu ứng bắn
    }
    public void PlusFireRate(float chiso)
    {
        Debug.Log($"PlusFireRate được gọi với chiso = {chiso}");
        Debug.Log($"FireRate trước: {fireRate}");
        
        fireRate -= chiso;
        fireRate = Mathf.Max(fireRate, 0.1f);
        
        Debug.Log($"FireRate sau: {fireRate}");
    }
}