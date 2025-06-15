using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f; // Số viên/giây
    private float nextFireTime = 0f;
    private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();

        // Raycast kiểm tra có vật cản giữa chim và player không
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity);
        bool canSeePlayer = hit.collider != null && hit.collider.CompareTag("Player");

        // Di chuyển nếu xa hơn stopDistance và có thể nhìn thấy player
        if (distance > stopDistance && canSeePlayer)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        }
        // Nếu gần hơn stopDistance thì đứng yên

        // Quay mặt về phía player
        if (dir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // Tính tần suất bắn
        float fireInterval = 1f / fireRate;
        if (canSeePlayer && Time.time >= nextFireTime)
        {
            Fire(dir);
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void Fire(Vector3 direction)
    {
        // Thêm mã bắn đạn ở đây
    }
}
