using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingPanelController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    private bool isVisible = false;
    public TextMeshProUGUI currentHP;
    public TextMeshProUGUI currentEXP;
    public TextMeshProUGUI currentWeaponSpeed;
    public TextMeshProUGUI expBonusexpBonus;
    public RectTransform panel; // Kéo thả panel vào đây trong Inspector
    public float slideSpeed = 3000f; // Tốc độ trượt
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        // Vị trí panel khi ẩn (ngoài màn hình)
        hiddenPosition = new Vector2(visiblePosition.x + panel.rect.width, panel.anchoredPosition.y);
        // Vị trí panel khi hiện (ví dụ 0 hoặc vị trí mong muốn)
        visiblePosition = new Vector2(Screen.width - panel.rect.width, panel.anchoredPosition.y);

        // Đặt panel về vị trí ẩn ban đầu
        panel.anchoredPosition = hiddenPosition;
        SetCurrentIndexUI();
    }

    void Update()
    {
                SetCurrentIndexUI();

    }
    public void TogglePanel()
    {
        isVisible = !isVisible;
        StopAllCoroutines();
        StartCoroutine(SlidePanelWithPause(isVisible));
    }

    private IEnumerator SlidePanelWithPause(bool show)
    {
        Vector2 startPos = panel.anchoredPosition;
        Vector2 endPos = show ? visiblePosition : hiddenPosition;
        float elapsed = 0f;
        float duration = Vector2.Distance(startPos, endPos) / slideSpeed;

        while (elapsed < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        panel.anchoredPosition = endPos;

        // Khi panel đã hiện ra hoàn toàn thì mới dừng game
        if (show)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
    private void SetCurrentIndexUI()
    {
        if (playerController != null)
        {
            currentHP.text = $"{playerController.currentHealth}/{playerController.maxHealth}";
            currentEXP.text = $"{playerController.currentMana}/{playerController.maxMana}";
            currentWeaponSpeed.text = $"{playerController.rotationSpeed}";
            expBonusexpBonus.text = $"{playerController.expMultiplier * 100}%";
        }
    }
}
