using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private bool isPaused = false;
    private PlayerController playerController;
    public Slider healthSlider;
    public Slider manaSlider;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;
    public TextMeshProUGUI numberCandy, numberCandyShop;
    public GameObject pausePanel, uiShop; // Panel nền đen, UI shop
    public Button pauseButton; // Nút pause
    public Button shopButton, cancelShopButton; // Nút shop
    public Sprite pauseSprite; // Hình ảnh nút Pause
    public Sprite continueSprite; // Hình ảnh nút Continue
    public TextMeshProUGUI currentLevel;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        numberCandy.text = $"{playerController.totalCandyCurrent}";
        numberCandyShop.text = $"{playerController.totalCandyCurrent}";

        pauseButton.onClick.AddListener(TogglePause);
        UpdateButtonIcon();
        pausePanel.SetActive(false); // Ẩn panel nền đen lúc đầu
        shopButton.onClick.AddListener(ToggleShop); // Thêm sự kiện click cho nút shop
        uiShop.SetActive(false);     // Ẩn UI shop lúc đầu
        cancelShopButton.onClick.AddListener(CloseShop); // Thêm sự kiện click cho nút đóng shop
    }
    void Update()
    {
        numberCandy.text = $"{playerController.totalCandyCurrent}";
        numberCandyShop.text = $"{playerController.totalCandyCurrent}";
    }
    public void UpdateHealth(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;
        hp.text = $"{current}/{max}";
    }

    public void UpdateMana(float current, float max)
    {
        manaSlider.maxValue = max;
        manaSlider.value = current;

        string displayText = $"{current}/{max}";
        mp.text = displayText;
    }

    public void UpdateLevel(float level)
    {
        currentLevel.text = $"{level}";
    }
        public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Tạm dừng game
            pausePanel.SetActive(true); // Hiện nền đen
        }
        else
        {
            Time.timeScale = 1f; // Tiếp tục game
            pausePanel.SetActive(false); // Ẩn nền đen
        }

        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (isPaused)
        {
            pauseButton.image.sprite = continueSprite;
        }
        else
        {
            pauseButton.image.sprite = pauseSprite;
        }
    }

    public void ToggleShop()
    {
        uiShop.SetActive(!uiShop.activeSelf);
        Time.timeScale = uiShop.activeSelf ? 0f : 1f; // Dừng game khi shop mở, tiếp tục khi shop tắt
    }

    public void OpenShop()
    {
        uiShop.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        uiShop.SetActive(false);
        Time.timeScale = 1f;
    }
}
