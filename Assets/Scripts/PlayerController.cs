using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float moveSpeed = 5f; // toc do di chuyen cua nhan vat
    //[SerializeField] private Rigidbody2D rb; // bien luu Rigidbody2D cua nhan vat
    //private Vector2 movement; // bien luu huong di chuyen cua nhan vat
    //public float dashDistance = 2f; // khoach cach dich chuyen
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>(); // Lay Rigidbody2D cua nhan vat
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    movement.x = Input.GetAxisRaw("Horizontal"); //di chuyen tren truc x
    //    movement.y = Input.GetAxisRaw("Vertical"); // di chuyen tren truc y

    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        Dash();
    //    }
    //}
    //void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    //}
    //public void Dash()
    //{
    //    if (movement != Vector2.zero) // Kiem tra neu nhan vat dang di chuyen
    //    {
    //        rb.position += movement.normalized * dashDistance; // Dich chuyen nhan vat theo huong di chuyen
    //    }
    //}


    [SerializeField] private MobileJoystick playerJoystick; // bien luu tham chieu den MobileJoystick
    private Rigidbody2D rig;
    [SerializeField] private float moveSpeed; // toc do di chuyen cua nhan vat
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }
}

