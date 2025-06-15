using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private PlayerController playerController;
    public Image btnBuyDev, btnBuyPop, btnBuySmirk, btnBuySnark; //mua
    public Image btnWearDev, btnWearPop, btnWearSmirk, btnWearSnark; //mac
    public SpriteRenderer spriteRendererPlayer; // Hình ảnh của người chơi
    public Sprite Dev, Pop, Smirk, Snark; //hinh anh cac trang phuc
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        btnBuyPop.GetComponent<Button>().onClick.AddListener(BuyPop);
        btnWearPop.gameObject.SetActive(false); // Ẩn hình mặc lúc đầu

        btnBuySmirk.GetComponent<Button>().onClick.AddListener(BuySmirk);
        btnWearSmirk.gameObject.SetActive(false); // Ẩn hình mặc lúc đầu

        btnBuySnark.GetComponent<Button>().onClick.AddListener(BuySnark);
        btnWearSnark.gameObject.SetActive(false); // Ẩn hình mặc lúc đầu

        btnWearDev.GetComponent<Button>().onClick.AddListener(WearDev);
        btnWearPop.GetComponent<Button>().onClick.AddListener(WearPop);
        btnWearSmirk.GetComponent<Button>().onClick.AddListener(WearSmirk);
        btnWearSnark.GetComponent<Button>().onClick.AddListener(WearSnark);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WearDev()
    {
        if (spriteRendererPlayer != null && Dev != null)
        {
            spriteRendererPlayer.sprite = Dev; // Đổi hình ảnh của người chơi sang Dev
        }
        else
        {
            Debug.LogWarning("SpriteRenderer không được gán cho ShopController.");
        }
    }
    public void BuyPop()
    {
        if (playerController.totalCandyCurrent >= 1)
        {
            playerController.totalCandyCurrent -= 1;
            btnBuyPop.gameObject.SetActive(false);
            btnWearPop.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Không đủ candy để mua Pop :(( hic");
        }
    }
public void WearPop()
{
    Debug.Log("WearPop được gọi");
    if (spriteRendererPlayer != null && Pop != null)
    {
        spriteRendererPlayer.sprite = Pop;
        Debug.Log("Sprite đã được thay đổi thành: " + Pop.name);
    }
    else
    {
        Debug.LogWarning("SpriteRenderer hoặc Pop đang là null");
    }
}
    public void BuySmirk()
    {
        if (playerController.totalCandyCurrent >= 1)
        {
            playerController.totalCandyCurrent -= 1;
            btnBuySmirk.gameObject.SetActive(false);
            btnWearSmirk.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Không đủ candy để mua Smirk :(( hic");
        }
    }
    public void WearSmirk()
    {
        if (spriteRendererPlayer != null && Smirk != null)
        {
            spriteRendererPlayer.sprite = Smirk; // Đổi hình ảnh của người chơi sang Smirk
        }
        else
        {
            Debug.LogWarning("SpriteRenderer không được gán cho ShopController.");
        }
    }
    public void BuySnark()
    {
        if (playerController.totalCandyCurrent >= 1)
        {
            playerController.totalCandyCurrent -= 1;
            btnBuySnark.gameObject.SetActive(false);
            btnWearSnark.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Không đủ candy để mua Snark :(( hic");
        }
    }
    public void WearSnark()
    {
        if (spriteRendererPlayer != null && Snark != null)
        {
            spriteRendererPlayer.sprite = Snark; // Đổi hình ảnh của người chơi sang Snark
        }
        else
        {
            Debug.LogWarning("SpriteRenderer không được gán cho ShopController.");
        }
    }
}
