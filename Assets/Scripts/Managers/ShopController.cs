using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private PlayerController playerController;
    public Image btnBuyDev, btnBuyPop, btnBuySmirk, btnBuySnark; //mua
    public Image btnWearDev, btnWearPop, btnWearSmirk, btnWearSnark; //mac
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        btnBuyPop.GetComponent<Button>().onClick.AddListener(BuyPop);
        btnWearPop.gameObject.SetActive(false); // Ẩn hình mặc lúc đầu

        btnBuySmirk.GetComponent<Button>().onClick.AddListener(BuySmirk);
        btnWearSmirk.gameObject.SetActive(false); // Ẩn hình mặc lúc đầu

        btnBuySnark.GetComponent<Button>().onClick.AddListener(BuySnark);
        btnWearSnark.gameObject.SetActive(false); // Ẩn hình mặc lúc đầu
    }

    // Update is called once per frame
    void Update()
    {

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
}
