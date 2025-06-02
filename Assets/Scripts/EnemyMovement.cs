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
    [SerializeField] private float expEnemy;

    [Header("Effect")]
    [SerializeField] private ParticleSystem enemyDieEffect;

    [Header("Debug")]
    [SerializeField] private bool gizmos;
    private PlayerController playerController;
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
            PassAway();
        }
    }
    private void PassAway()
    {
        enemyDieEffect.transform.SetParent(null);
        enemyDieEffect.Play();

        Destroy(gameObject);
        playerController.TotalEXPPlayer(5f);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon1"))
        {
            PassAway();
            playerController.TotalEXPPlayer(5f);
        }
    }
}
