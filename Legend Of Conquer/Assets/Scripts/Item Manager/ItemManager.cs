using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType { Item, Weapon, Armor };
    public ItemType itemType;

    public string itemName, itemDescription;
    public int value;
    public Sprite itemsImage;

    public enum AffectType { HP, Mana };
    public AffectType affectType;

    public int weaponDexterity;
    public int armorDefence;

    public bool isStackable;
    public int amount;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("This item is " + itemName);
            Inventory.instance.AddItems(this);
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
