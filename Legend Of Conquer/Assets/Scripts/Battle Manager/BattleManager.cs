using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();
    [SerializeField] Transform[] playerPositions, enemyPositions;
    [SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartBattle(new string[] { "Mage", "Warlock" });
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            BattleSceneSetup();
            PlayerSetup();
            EnemiesSetup(enemiesToSpawn);
        }
    }

    public void BattleSceneSetup()
    {
    isBattleActive = true;
    GameManager.instance.battleActive = true;
    battleScene.SetActive(true);
    transform.position = new Vector3(
        Camera.main.transform.position.x,
        Camera.main.transform.position.y,
        Camera.main.transform.position.z);
    }

    public void PlayerSetup()
    {
        for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++)
        {
            if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    if (playerPrefabs[j].characterName == GameManager.instance.GetPlayerStats()[i]._playerName)
                    {
                        BattleCharacters newPlayer = Instantiate(playerPrefabs[j],
                            playerPositions[i].position,
                            playerPositions[i].rotation,
                            playerPositions[i]);
                        activeCharacters.Add(newPlayer);

                        PlayerStats player = GameManager.instance.GetPlayerStats()[i];
                        activeCharacters[i].maxHp = player._maxHP;
                        activeCharacters[i].currentHp = player._currentHP;

                        activeCharacters[i].maxMana = player._maxMana;
                        activeCharacters[i].currentMana = player._currentMana;

                        activeCharacters[i].dexterity = player._dexterity;
                        activeCharacters[i].defence = player._defence;

                        activeCharacters[i].wpnPower = player.weaponDex;
                        activeCharacters[i].armorDefence = player.armorDef;
                    }

                }
            }
        }
    }

    private void EnemiesSetup(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemiesPrefabs.Length; j++)
                {
                    if (enemiesToSpawn[i] == enemiesPrefabs[j].characterName)
                    {
                        BattleCharacters newEnemy = Instantiate(
                            enemiesPrefabs[j],
                            enemyPositions[i].position,
                            enemyPositions[i].rotation,
                            enemyPositions[i]
                            );
                        activeCharacters.Add(newEnemy);
                    }
                }
            }
        }
    }
}
