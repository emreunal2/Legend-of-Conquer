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
            print(playerStats[i]._neededXP[playerStats[i]._playerLevel]);
            characterPanel[i].SetActive(true);
        }
    }
    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");

    }
}
