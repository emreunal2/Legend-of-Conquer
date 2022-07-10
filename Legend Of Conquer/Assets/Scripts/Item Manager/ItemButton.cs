using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemManager itemOnButton;
    public static ItemButton instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (MenuMenager.instance.menu.activeInHierarchy)
        {
            MenuMenager.instance.itemName.text = itemOnButton.itemName.ToString();
            MenuMenager.instance.itemDescription.text = itemOnButton.itemDescription.ToString();
            MenuMenager.instance.activeItem = itemOnButton;
        }

        if (ShopManager.instance.shopMenu.activeInHierarchy)
        {
            if (ShopManager.instance.buyPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedBuyItem(itemOnButton);
            }
            else if (ShopManager.instance.sellPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedSellItem(itemOnButton);
            }
        }
    }
}
