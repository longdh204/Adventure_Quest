using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;

    [Header("Debug")]
    [SerializeField] private bool gizmos;
    void Start()
    {
        player = FindObjectOfType<Player>();

        if(player == null)
        {
            Debug.LogError("Khong co player quai tu destroy");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        FollowPlayer();
        TryAttack();
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
        if(distanceToPlayer <= playerDetectionRadius)
        {
            // Attack logic here
            Debug.Log("Enemy is attacking the player!");
            Destroy(gameObject); // Destroy the enemy after attacking
        }
    }
    // hàm này tạo 1 quả cầu đỏ để hiển thị phạm vi phát hiện người chơi
    private void OnDrawGizmos()
    {
        if (!gizmos)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
