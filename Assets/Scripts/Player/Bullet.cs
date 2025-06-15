using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script cho viên đạn với pooling support
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f; // Thời gian sống của viên đạn
    private Rigidbody2D rb;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Được gọi khi viên đạn được kích hoạt từ pool
    public void Initialize(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        transform.up = direction;
        
        // Reset rigidbody state
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        // Set new velocity
        rb.velocity = direction * speed;
        timer = 0f;
        gameObject.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Thay vì Destroy, trả về pool
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        // Reset rigidbody trước khi return về pool
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        gameObject.SetActive(false);
        BulletPool.Instance.ReturnBullet(this);
    }
}