using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] PlayerStats[] playerStats;
    public bool gameMenuOpened, dialogBoxOpened, shopMenuOpened;
    public int currentCoin;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        playerStats = FindObjectsOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpened || dialogBoxOpened || shopMenuOpened)
        {
            Player.instance.deactivatedMovement = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Saved");
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Loaded");
            LoadData();
        }

        else
        {
            Player.instance.deactivatedMovement = false;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);
        for(int i=0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_active", 0);
            }
            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_Level", playerStats[i]._playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_CurrentXP", playerStats[i]._currentXP);

            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_MaxHP", playerStats[i]._maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_MaxMana", playerStats[i]._maxMana);

            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_CurrentHp", playerStats[i]._currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_CurrentMana", playerStats[i]._currentMana);

            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_Dexterity", playerStats[i]._dexterity);
            PlayerPrefs.SetInt("Player_" + playerStats[i]._playerName + "_Defence", playerStats[i]._defence);

            PlayerPrefs.SetString("Player_" + playerStats[i]._playerName + "_Weapon", playerStats[i].equippedWeaponName);
            PlayerPrefs.SetString("Player_" + playerStats[i]._playerName + "_Armor", playerStats[i].equippedArmorName);
        }
    }

    public void LoadData()
    {
        Player.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Pos_X"),
            PlayerPrefs.GetFloat("Player_Pos_Y"),
            PlayerPrefs.GetFloat("Player_Pos_Z")
            );

        for(int i=0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerStats[i]._playerName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }

        }
    }
}
