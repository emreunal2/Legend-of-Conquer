using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    [SerializeField] string playerName;
    [SerializeField] int playerLevel = 1;
    [SerializeField] int maxLevel = 50;

    [SerializeField] int currentXP;
    [SerializeField] int[] neededXP;
    private int baseNeededXP = 100;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int maxMana = 30;
    [SerializeField] int currentMana;

    [SerializeField] int dexterity;
    [SerializeField] int defence;

    public string equippedWeaponName;
    public string equippedArmorName;

    public int weaponDex;
    public int armorDef;

    public string _playerName{
       get {return playerName;}
    }
    public int _playerLevel
    {
        get { return playerLevel;}
    }
    public int _maxHP
    {
        get { return maxHP; }
    }
    public int _currentHP
    {
        get { return currentHP; }
    }
    public int _maxMana
    {
        get { return maxMana; }
    }
    public int _currentMana
    {
        get { return currentMana; }
    }
    public int _currentXP
    {
        get { return currentXP; }
    }
    public int[] _neededXP
    {
        get { return neededXP; }
    }
    public int _dexterity
    {
        get { return dexterity; }
    }
    public int _defence
    {
        get { return defence; }
    }

    public ItemManager equipedArmor, equipedWeapon;




    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        neededXP = new int[maxLevel];
        neededXP[1] = baseNeededXP;
        for (int i = 2; i < neededXP.Length; i++)
        {
            neededXP[i] = (int)(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddXp(200);
        }
    }

    public void AddXp(int amountXP)
    {
        currentXP += amountXP;
        if (currentXP > neededXP[playerLevel])
        {
            currentXP -= neededXP[playerLevel];
            playerLevel++;
            maxHP = Mathf.FloorToInt(maxHP * 1.06f);
            currentHP = maxHP;

            maxMana = Mathf.FloorToInt(maxMana * 1.06f);
            currentMana = maxMana;
            if (playerLevel % 2 == 0)
            {
                dexterity++;
            }
            else
            {
                defence++;
            }
        }

    }

    public void AddHP(int amountHP)
    {
        currentHP += amountHP;
        if (currentHP > maxHP)
        {
        currentHP = maxHP;
        }
        
    }

    public void AddMana(int amountMana)
    {
        currentMana += amountMana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void EquipWeapon(ItemManager weaponToEquip)
    {
        equipedWeapon = weaponToEquip;
        equippedWeaponName = weaponToEquip.itemName;
        weaponDex = weaponToEquip.weaponDexterity;
    }

    public void EquipArmor(ItemManager armorToEquip)
    {
        equipedArmor = armorToEquip;
        equippedArmorName = armorToEquip.itemName;
        armorDef = armorToEquip.armorDefence;
    }

}
