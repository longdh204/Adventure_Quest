using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Image[] upgradeImages;
    void Start()
    {
        foreach (Image img in upgradeImages)
        {
            Button button = img.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {

            });
        }
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
        // Handle the upgrade selection logic here
        Debug.Log("Upgrade selected: " + selectedImage.name);
        // Hide the upgrade options after selection
        // Ẩn các tùy chọn sau khi chọn
        foreach (Image img in upgradeImages)
        {
            img.gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
        // Thêm logic nâng cấp cho người chơi
    }
}
