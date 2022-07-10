using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] TextMeshProUGUI currentCoin;

    public List<ItemManager> itemsForSale;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotBuyContainerParent;
    [SerializeField] Transform itemSlotSellContainerParent;

    [SerializeField] ItemManager selectedItem;
    [SerializeField] TextMeshProUGUI buyItemName,buyItemDescription,buyItemValue;
    [SerializeField] TextMeshProUGUI sellItemName,sellItemDescription,sellItemValue;
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
        UpdateItemsInSlot(itemSlotBuyContainerParent, itemsForSale);
    }
    public void OpenSellPanel()
    {
        sellPanel.SetActive(true);
        buyPanel.SetActive(false);
        UpdateItemsInSlot(itemSlotSellContainerParent, Inventory.instance.GetItemsList());
    }

    private void UpdateItemsInSlot(Transform itemSlotContainerParent, List<ItemManager> itemsToLook)
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemManager item in itemsToLook)
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();
            Image itemImage = itemSlot.Find("Item Image").GetComponent<Image>();
            Text itemText = itemSlot.Find("Text").GetComponent<Text>();
            itemImage.sprite = item.itemsImage;
            if (item.amount > 1)
            {
                itemText.text = "";
            }
            else { itemText.text = ""; }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void SelectedBuyItem(ItemManager itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value:" + selectedItem.value;

    }

    public void SelectedSellItem(ItemManager itemToSell)
    {
        selectedItem = itemToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value:" + selectedItem.value;
    }

    public void BuyItem()
    {
        if (GameManager.instance.currentCoin >= selectedItem.amount)
        {
            Inventory.instance.AddItems(selectedItem);
            GameManager.instance.currentCoin -= selectedItem.value;

            currentCoin.text = GameManager.instance.currentCoin.ToString();
        }
    }

    public void SellItem()
    {
        if (selectedItem)
        {
            GameManager.instance.currentCoin += selectedItem.value;
            Inventory.instance.RemoveItem(selectedItem);
            UpdateItemsInSlot(itemSlotSellContainerParent, Inventory.instance.GetItemsList());
            currentCoin.text = GameManager.instance.currentCoin.ToString();
            selectedItem = null;
        }
    }
}
