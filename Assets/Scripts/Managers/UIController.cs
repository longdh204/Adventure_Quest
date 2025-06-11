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

    [Header("Level")]
    public TextMeshProUGUI currentLevel;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        numberCandy.text = $"{playerController.totalCandyCurrent}";
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
}
