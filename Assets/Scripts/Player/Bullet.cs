using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // Set the bullet's velocity in the direction it's facing
    }

    // Update is called once per frame
    void Update()
    {

    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Kiểm tra nếu va chạm với quái
        {
            Destroy(gameObject); // Xóa viên đạn
        }
    }
}
