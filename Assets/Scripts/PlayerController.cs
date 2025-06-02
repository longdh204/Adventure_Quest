using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MobileJoystick playerJoystick; // bien luu tham chieu den MobileJoystick
    private Rigidbody2D rig;
    [SerializeField] private float moveSpeed; // toc do di chuyen cua nhan vat
    [SerializeField] private float detectionRadius = 5f; // ban kinh phat hien va cham
    [SerializeField] private GameObject weapon;
    [SerializeField] private float rotationSpeed = 100f; // thoi gian giua cac lan tan cong
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckForEnemies();
        RotationWeapon();
    }
    private void FixedUpdate()
    {
        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }
    private void CheckForEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Logic khi va cham voi ke thu
                Debug.Log("Đã phát hiện kẻ thù trong bán kính" + hitCollider.name);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void RotationWeapon()
    {
        if (weapon != null)
        {
            weapon.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}

