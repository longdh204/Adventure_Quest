using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerController playerController;
    [Header("Health and EXP")]
    public Slider healthSlider;
    public Slider manaSlider;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;

    public TextMeshProUGUI numberCandy;
    public GameObject pausePanel; // Panel nền đen
    public Button pauseButton; // Nút pause
    public Sprite pauseSprite; // Hình ảnh nút Pause
    public Sprite continueSprite; // Hình ảnh nút Continue
    private bool isPaused = false;

    [Header("Level")]
    public TextMeshProUGUI currentLevel;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        numberCandy.text = $"{playerController.totalCandyCurrent}";

        pauseButton.onClick.AddListener(TogglePause);
        UpdateButtonIcon();
        pausePanel.SetActive(false); // Ẩn panel nền đen lúc đầu
    }
    void Update()
    {
        numberCandy.text = $"{playerController.totalCandyCurrent}";
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
}
