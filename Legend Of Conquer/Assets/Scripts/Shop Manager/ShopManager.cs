using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] TextMeshProUGUI currentCoin;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShopMenu()
    {
        shopMenu.SetActive(true);
        GameManager.instance.shopMenuOpened = true;
        currentCoin.text = "Current Coin:" + GameManager.instance.currentCoin;
    }
    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopMenuOpened = false;
    }
    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);
    }
    public void OpenSellPanel()
    {
        sellPanel.SetActive(true);
        buyPanel.SetActive(false);
    }
}
