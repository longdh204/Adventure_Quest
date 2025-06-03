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
            button.onClick.AddListener(() =>
            {

            });
        }
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {

    }
    public void ShowUpgradeOptions()
    {
        foreach (Image img in upgradeImages)
        {
            img.gameObject.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    public void OnUpgradeSelected(Image selectedImage)
    {
        if(selectedImage == upgradeImages[0])
        {
            UpdateHeath();
        }
        Debug.Log("Upgrade selected: " + selectedImage.name);

        foreach (Image img in upgradeImages)
        {
            img.gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
    }
    public void UpdateHeath()
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
}
