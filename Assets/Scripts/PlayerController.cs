using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MobileJoystick playerJoystick; // bien luu tham chieu den MobileJoystick
    private Rigidbody2D rig;
    [SerializeField] private float moveSpeed; // toc do di chuyen cua nhan vat
    [SerializeField] private float detectionRadius = 5f; // ban kinh phat hien va cham
    [SerializeField] private GameObject weapon;
    [SerializeField] private float rotationSpeed = 100f; // thoi gian giua cac lan tan cong
    public Slider healthSlider; // thanh mau cua nhan vat
    [SerializeField] private Slider manaSlider; // thanh mau cua nhan vat
    
    [Header ("Health and EXP")]    
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;
    public float maxHealth = 100f; // Giá trị tối đa sức khỏe
    public float currentHealth;
    public float maxMana = 100f; // Giá trị tối đa năng lượng
    public float currentMana;

    [Header("Level")]
    public float levelPlayer = 1;
    public TextMeshProUGUI currentLevel;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentMana = 0f;
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = currentHealth;
        manaSlider.maxValue = maxMana; 
        manaSlider.value = currentMana;

        // level
        currentLevel.text = $"{levelPlayer}";
    }
    private void Update()
    {
        // enemy
        CheckForEnemies();
        RotationWeapon();

        //hp va exp
        hp.text = $"{currentHealth}/{maxHealth}";
        mp.text = $"{currentMana}/{maxMana}";
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
    private void TotalHealthPlayer(float damage)
    {
        currentHealth -= damage; // Giảm sức khỏe hiện tại theo lượng sát thương
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Đảm bảo sức khỏe không âm
        healthSlider.value = currentHealth; // Cập nhật thanh máu
        if(currentHealth <= 0)
        {
            Debug.Log("Nhân vật đã chết");
            //Die(); // Gọi hàm xử lý khi nhân vật chết
        }
    }
    public void TotalEXPPlayer(float exp)
    {
        currentMana += exp; 
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Đảm bảo sức khỏe không âm
        manaSlider.value = currentMana; // Cập nhật thanh máu
        if (currentMana >= 100)
        {
            //Debug.Log("Nhân vật đã lên cấp");
            //Die(); // Gọi hàm xử lý khi nhân vật chết
            FindObjectOfType<UpgradeManager>().ShowUpgradeOptions(); // Hiển thị tùy chọn nâng cấp
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TotalHealthPlayer(0f);
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth; // Hồi đầy máu
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        hp.text = $"{currentHealth}/{maxHealth}";
        currentMana = 0;
        LevelUP(1);
    }
    public void LevelUP(float levelUp)
    {
        levelPlayer += levelUp;
        currentLevel.text = $"{levelPlayer}";
    }
}

