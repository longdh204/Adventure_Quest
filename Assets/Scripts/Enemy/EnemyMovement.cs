using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private float expEnemy;
    [SerializeField] private ParticleSystem enemyDieEffect;
    [SerializeField] private bool gizmos;
    private PlayerController playerController;

    private bool isDead = false; // Thêm flag để tránh double trigger

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Khong co player quai tu destroy");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isDead) // Chỉ di chuyển khi chưa chết
        {
            FollowPlayer();
            TryAttack();
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            // Chạm vào player → gây damage cho player
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (!isDead && playerController != null)
        {
            // Gây damage cho player thay vì destroy enemy
            Debug.Log("Enemy tấn công player!");
            // Nếu bạn muốn enemy gây damage thì uncomment dòng dưới:
            // playerController.TotalHealthPlayer(10f);

            // Hoặc nếu muốn enemy chết khi chạm player:
            PassAway();
        }
    }

    private void PassAway()
    {
        if (isDead) return; // Tránh gọi nhiều lần

        isDead = true;
        enemyDieEffect.transform.SetParent(null);
        enemyDieEffect.Play();

        // Chỉ cộng EXP khi enemy chết do weapon, không phải do chạm player
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return; // Tránh trigger khi đã chết

        if (collision.CompareTag("Weapon1"))
        {
            Debug.Log("Enemy bị weapon tiêu diệt!");
            PassAway();
            if (playerController != null)
            {
                playerController.TotalEXPPlayer(5f);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy chạm vào Player!");
            // Gây damage cho player
            if (playerController != null)
            {
                playerController.TotalHealthPlayer(10f); // Gọi hàm public
            }
            PassAway(); // Enemy chết sau khi tấn công
        }
    }
}