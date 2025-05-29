using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // toc do di chuyen cua nhan vat
    [SerializeField] private Rigidbody2D rb; // bien luu Rigidbody2D cua nhan vat
    private Vector2 movement; // bien luu huong di chuyen cua nhan vat
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lay Rigidbody2D cua nhan vat
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //di chuyen tren truc x
        movement.y = Input.GetAxisRaw("Vertical"); // di chuyen tren truc y
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
