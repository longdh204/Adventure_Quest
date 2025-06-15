using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object Pool Manager cho bullets
public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    
    [Header("Pool Settings")]
    public GameObject bulletPrefab;
    public int poolSize = 50;
    
    private Queue<Bullet> bulletPool = new Queue<Bullet>();
    private List<Bullet> activeBullets = new List<Bullet>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializePool()
    {
        // Tạo trước các bullets và đưa vào pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public Bullet GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            Bullet bullet = bulletPool.Dequeue();
            activeBullets.Add(bullet);
            return bullet;
        }
        else
        {
            // Nếu pool hết, tạo bullet mới
            GameObject bulletObj = Instantiate(bulletPrefab);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            activeBullets.Add(bullet);
            return bullet;
        }
    }

    public void ReturnBullet(Bullet bullet)
    {
        if (activeBullets.Contains(bullet))
        {
            activeBullets.Remove(bullet);
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    // Utility method để lấy thông tin pool
    public int GetActiveCount() => activeBullets.Count;
    public int GetPoolCount() => bulletPool.Count;
}
