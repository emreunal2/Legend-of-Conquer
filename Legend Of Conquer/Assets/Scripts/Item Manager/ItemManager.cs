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
    public int ammountOfEffect;
    public AffectType affectType;

    public int weaponDexterity;
    public int armorDefence;

    public bool isStackable;
    public int amount;

    public void UseItem(int charToUseON)
    {
        PlayerStats selectedPlayer = GameManager.instance.GetPlayerStats()[charToUseON];
        if(itemType == ItemType.Item)
        {
            if (affectType == AffectType.HP)
            {
                selectedPlayer.AddHP(ammountOfEffect);
            }
            else if (affectType == AffectType.Mana)
            {
                selectedPlayer.AddMana(ammountOfEffect);
            }
        }
        else if (itemType == ItemType.Weapon)
        {
            if (selectedPlayer.equippedWeaponName != "")
            {
                Inventory.instance.AddItems(selectedPlayer.equipedWeapon);
            }
            selectedPlayer.EquipWeapon(this);
        }
        else if (itemType == ItemType.Armor)
        {
            if (selectedPlayer.equippedArmorName != "")
            {
                Inventory.instance.AddItems(selectedPlayer.equipedArmor);
            }
            selectedPlayer.EquipArmor(this);
        }
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
