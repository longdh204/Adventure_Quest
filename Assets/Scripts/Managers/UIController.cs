using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Health and EXP")]
    public Slider healthSlider;
    public Slider manaSlider;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;

    [Header("Level")]
    public TextMeshProUGUI currentLevel;

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
        mp.text = $"{current}/{max}";
    }

    public void UpdateLevel(float level)
    {
        currentLevel.text = $"{level}";
    }
}
