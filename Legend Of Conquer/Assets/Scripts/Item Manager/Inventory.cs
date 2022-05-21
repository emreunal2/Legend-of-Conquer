using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private List<ItemManager> itemsList;
    
    void Start()
    {
        instance = this;
        itemsList = new List<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItems(ItemManager item)
    {
        if (item.isStackable)
        {
            bool isItemExits = false;
            foreach (ItemManager i in itemsList)
            {

                if (i.itemName == item.itemName)
                {
                    i.amount += item.amount;
                    isItemExits = true;
                }

            }
            if (!isItemExits)
            {
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
    }

    public void RemoveItem(ItemManager item)
    {
        if (item.isStackable)
        {
            ItemManager inventoryItem = null;

            foreach(ItemManager i in itemsList)
            {
                if(i.itemName == item.itemName)
                {
                    i.amount--;
                    inventoryItem = i;
                }
            }
            if(inventoryItem != null && inventoryItem.amount <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
            
        }
        else
        {
            itemsList.Remove(item);
        }
        MenuMenager.instance.UpdateItemsInventory();
    }
    public List<ItemManager> GetItemsList()
    {
        return itemsList;
    }
}
