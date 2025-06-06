using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Image[] upgradeImages;
    private PlayerController playerController;

    void Start()
    {
        foreach (Image img in upgradeImages)
        {
            Button button = img.GetComponent<Button>();
            button.onClick.AddListener(() => OnUpgradeSelected(img));
        }

        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
    }
    public void ShowUpgradeOptions(List<int> imageIndexes)
    {
        for (int i = 0; i < upgradeImages.Length; i++)
        {
            upgradeImages[i].gameObject.SetActive(imageIndexes.Contains(i));
        }
        Time.timeScale = 0f;
    }

    public void OnUpgradeSelected(Image selectedImage)
    {
        if (selectedImage == upgradeImages[0])
        {
            UpdateHealth();
        }
        else if (selectedImage == upgradeImages[1])
        {
            // Tăng 10% EXP
            UpdateEXPBonus();
        }else if (selectedImage == upgradeImages[2])
        {
            UpdateSpeedWeapon();
        }

        Debug.Log("Upgrade selected: " + selectedImage.name);

        foreach (Image img in upgradeImages)
        {
            img.gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void UpdateHealth()
    {
        if (playerController != null)
        {
            playerController.IncreaseMaxHealth(100f);
        }
        else
        {
            Debug.LogWarning("Lỗi cộng máu :((");
        }
    }
    public void UpdateEXPBonus()
    {
        if (playerController != null)
        {
            playerController.IncreaseEXPMultiplier(0.1f);
        }
        else
        {
            Debug.LogWarning("Lỗi tăng EXP :((");
        }
    }
    public void UpdateSpeedWeapon()
    {
        if (playerController != null)
        {
            playerController.IncreaseWeapon(100f);
        }
        else
        {
            Debug.LogWarning("Lỗi cộng tốc độ quay của vũ khí :((");
        }
    }
}