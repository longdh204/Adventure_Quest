using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private MobileJoystick playerJoystick;
    private Rigidbody2D rig;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectionRadius = 5f; // ban kinh phat hien va cham
    [SerializeField] private GameObject weapon;
    public float rotationSpeed = 100f;

    public float maxHealth = 100f;
    public float currentHealth;
    public float maxMana = 100f;
    public float currentMana;
    public float levelPlayer = 1;

    public float expMultiplier = 1f; // Bắt đầu từ 1 (100%)

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentMana = 0f;
        uiController.UpdateHealth(currentHealth, maxHealth);
        uiController.UpdateMana(currentMana, maxMana);
        uiController.UpdateLevel(levelPlayer);
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
                //Debug.Log("Đã phát hiện kẻ thù trong bán kính" + hitCollider.name);
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

    public void TotalHealthPlayer(float damage)
    {
        currentHealth -= damage; // Giảm sức khỏe hiện tại theo lượng sát thương
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Đảm bảo sức khỏe không âm
        uiController.UpdateHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Nhân vật đã chết");
            //Die(); // Gọi hàm xử lý khi nhân vật chết
        }
    }

    public void TotalEXPPlayer(float exp)
    {
        // Áp dụng multiplier cho EXP
        float finalExp = exp * expMultiplier;

        currentMana += finalExp;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        uiController.UpdateMana(currentMana, maxMana);

        if (currentMana >= 100)
        {
            LevelUP(1); // Tăng cấp độ khi mana đạt 100
            //Debug.Log("Nhân vật đã lên cấp");
            if (levelPlayer < 5)
            {
                // chọn mặc định ảnh 0, 1, 22
                FindObjectOfType<UpgradeManager>().ShowUpgradeOptions(new List<int> { 0, 1, 2 }); // Hiển thị tùy chọn nâng cấp
                // Hoặc ngẫu nhiên 3 ảnh bất kỳ nếu muốn
                // List<int> allIndexes = new List<int>();
                // for (int i = 0; i < FindObjectOfType<UpgradeManager>().upgradeImages.Length; i++)
                //     allIndexes.Add(i);
                // var randomIndexes = allIndexes.OrderBy(x => Random.value).Take(3).ToList();
                // FindObjectOfType<UpgradeManager>().ShowUpgradeOptions(randomIndexes);
            }
            else if (levelPlayer >= 5 && levelPlayer < 10)
            {
                // Chọn mặc định ảnh 0, 1, 2
                FindObjectOfType<UpgradeManager>().ShowUpgradeOptions(new List<int> { 3, 4, 5 }); // Hiển thị tùy chọn nâng cấp
            }
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        uiController.UpdateHealth(currentHealth, maxHealth);
        currentMana = 0;
        uiController.UpdateMana(currentMana, maxMana);
    }
    // Thêm hàm mới để tăng EXP multiplier
    public void IncreaseEXPMultiplier(float bonusPercent)
    {
        expMultiplier += bonusPercent; // bonusPercent = 0.1f để tăng 10%
        currentMana = 0;
        uiController.UpdateMana(currentMana, maxMana);
        Debug.Log("EXP Multiplier hiện tại: " + (expMultiplier * 100) + "%");
    }

    public void IncreaseWeapon(float updateSpeedWeapon)
    {
        rotationSpeed += updateSpeedWeapon;
        currentHealth = maxHealth;
        uiController.UpdateHealth(currentHealth, maxHealth);
        currentMana = 0;
        uiController.UpdateMana(currentMana, maxMana);
    }
    public void LevelUP(float levelUp)
    {
        levelPlayer += levelUp;
        uiController.UpdateLevel(levelPlayer);
    }
}