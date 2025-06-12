using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private float expEnemy;
    [SerializeField] private ParticleSystem enemyDieEffect;
    [SerializeField] private bool gizmos;
    private PlayerController playerController;
    private Player player;
    private bool isDead = false;
    public GameObject itemDropPrefab;
    public float dropChance = 0.5f; // Tỷ lệ rơi vật phẩm (50%)
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
        // Tạo vật phẩm rơi ra tại vị trí quái chết
        if (itemDropPrefab != null && Random.value <= dropChance)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }
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
            float damage = 10f;
            if (CompareTag("Enemy")) damage = 10f;
            else if (CompareTag("Enemy2")) damage = 20f;
            else if (CompareTag("Enemy3")) damage = 30f;
            if (playerController != null)
            {
                playerController.TotalHealthPlayer(damage);
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