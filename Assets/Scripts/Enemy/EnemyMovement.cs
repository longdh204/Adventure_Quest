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

    private bool isDead = false;

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
        if (!isDead)
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
            PassAway();
        }
    }

    private void PassAway()
    {
        if (isDead) return;
        isDead = true;
        enemyDieEffect.transform.SetParent(null);
        enemyDieEffect.Play();
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
        if (isDead) return;

        if (collision.CompareTag("Weapon1"))
        {
            PassAway();
            if (playerController != null)
            {
                playerController.TotalEXPPlayer(5f);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            if (playerController != null)
            {
                playerController.TotalHealthPlayer(10f);
            }
            PassAway();
        }else if( collision.CompareTag("Bullet"))
        {
            PassAway();
            if (playerController != null)
            {
                playerController.TotalEXPPlayer(5f);
            }
        }
    }
}