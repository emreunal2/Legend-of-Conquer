using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] BattleMoves[] battleMovesList;
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
            StartBattle(new string[] { "Mage", "Something" });
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        if (waitingForTurn)
        {
            if (activeCharacters[currentTurn].IsPlayer())
            {
                UIButtonHolder.SetActive(true);
            }
            else
            {
                UIButtonHolder.SetActive(false);
                StartCoroutine(EnemyMoveCoroutine());
            }
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            BattleSceneSetup();
            PlayerSetup();
            EnemiesSetup(enemiesToSpawn);

            waitingForTurn = true;
            currentTurn = 0;
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

    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeCharacters.Count)
        {
            currentTurn = 0;
        }
        waitingForTurn = true;
        UpdateBattle();
    }

    private void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayerDead = true;
        for(int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].currentHp <= 0)
            {
                // kill 
            }

            else
            {
                if (activeCharacters[i].IsPlayer())
                {
                    allPlayerDead = false;
                }

                else
                {
                    allEnemiesDead = false;
                }

            }
        }

        if (allPlayerDead || allEnemiesDead)
        {
            if (allEnemiesDead)
            {
                print("win");
            }
            else if (allPlayerDead)
            {
                print("lose");
            }

            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            isBattleActive = false;
        }

    }

    public IEnumerator EnemyMoveCoroutine()
    {
        waitingForTurn = false;
        yield return new WaitForSeconds(1f);

        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    private void EnemyAttack()
    {
        List<int> players = new List<int>();

        for(int i=0; i<activeCharacters.Count; i++)
        {
            if(activeCharacters[i].IsPlayer() && activeCharacters[i].currentHp > 0)
            {
                players.Add(i);
            }
        }

        int selectedPlayerToAttack = players[Random.Range(0, players.Count)];
        int selectedAttack = Random.Range(0, activeCharacters[currentTurn].AttackMovesAvailable().Length);

        for(int i=0; i < battleMovesList.Length; i++)
        {
           
            if (battleMovesList[i].moveName == activeCharacters[currentTurn].AttackMovesAvailable()[selectedAttack])
            {       
                Instantiate(
                    battleMovesList[i].moveEffect,
                    activeCharacters[selectedPlayerToAttack].transform.position,
                    activeCharacters[selectedPlayerToAttack].transform.rotation);
            }
        }
    }
}
