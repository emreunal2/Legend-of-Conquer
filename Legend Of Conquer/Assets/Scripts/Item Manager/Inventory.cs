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
        itemsList.Add(item);
    }
    public List<ItemManager> GetItemsList()
    {
        return itemsList;
    }
}
