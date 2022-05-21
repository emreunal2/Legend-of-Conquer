using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuMenager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;
    public static MenuMenager instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, lvlText, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;
    [SerializeField] GameObject[] statsButtons;
    [SerializeField] TextMeshProUGUI nameStats, hpStats, manaStats, dextarityStats, defenceStats;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;
    public TextMeshProUGUI itemName, itemDescription;

    public ItemManager activeItem;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
            }
            else
            {
                UpdateStats();
                menu.SetActive(true);
                GameManager.instance.gameMenuOpened = true;
            }
        }
    }
    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();
        for (int i = 0; i < playerStats.Length; i++)
        {
            nameText[i].text = playerStats[i]._playerName;
            hpText[i].text = "HP: " + playerStats[i]._currentHP + "/" + playerStats[i]._maxHP;
            manaText[i].text = "Mana: " + playerStats[i]._currentMana+ "/" + playerStats[i]._maxMana;
            lvlText[i].text = "Level: " + playerStats[i]._playerLevel;
            xpSlider[i].maxValue = playerStats[i]._neededXP[playerStats[i]._playerLevel];
            xpSlider[i].value = playerStats[i]._currentXP;
            xpText[i].text = playerStats[i]._currentXP.ToString() + "/" + playerStats[i]._neededXP[playerStats[i]._playerLevel].ToString();
            characterPanel[i].SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("exited game");
    }
    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");

    }

    public void StatsMenu()
    {
        for(int i = 0; i < playerStats.Length; i++)
        {
            statsButtons[i].SetActive(true);
            statsButtons[i].GetComponentInChildren<Text>().text = playerStats[i]._playerName;
        }
        StatsMenuUpdate(0);
    }

    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        PlayerStats playerSelected = playerStats[playerSelectedNumber];
        nameStats.text = playerSelected._playerName.ToString();
        hpStats.text = playerSelected._currentHP.ToString() + "/" + playerSelected._maxHP.ToString();
        manaStats.text = playerSelected._currentHP.ToString() + "/" + playerSelected._maxHP.ToString();
        dextarityStats.text = playerSelected._dexterity.ToString();
        defenceStats.text = playerSelected._defence.ToString();
    }

    public void UpdateItemsInventory()
    {
        foreach(Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();
            Image itemImage = itemSlot.Find("Item Image").GetComponent<Image>();
            Text itemText = itemSlot.Find("Text").GetComponent<Text>();
            itemImage.sprite = item.itemsImage;
            if (item.amount > 1)
            {
                itemText.text = item.amount.ToString();
            }
            else { itemText.text = ""; }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void DiscardItem()
    {
        print(activeItem.itemName);
        Inventory.instance.RemoveItem(activeItem);

    }
}
