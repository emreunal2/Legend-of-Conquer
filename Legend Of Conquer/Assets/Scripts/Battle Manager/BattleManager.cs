using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] BattleMoves[] battleMovesList;
    [SerializeField] GameObject battleScene;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();

    [SerializeField] Transform[] playerPositions, enemyPositions;
    [SerializeField] ParticleSystem attackEffect;
    [SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;

    [SerializeField] TextMeshProUGUI[] playersNameText;
    [SerializeField] Slider[] PlayerHealthSlider, PlayerManaSlider;
    [SerializeField] GameObject[] players;

    [SerializeField] GameObject enemyTargetPanel;
    public GameObject magicChoicePanel;
    [SerializeField] BattleTargetButtons[] targetButtons;
    [SerializeField] BattleMovesButton[] magicButtons;

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
            UpdateStats();
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
        UpdateStats();
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

        else
        {
            while (activeCharacters[currentTurn].currentHp <= 0)
            {
                currentTurn++;
                if (currentTurn >= activeCharacters.Count)
                {
                    currentTurn++;
                }
            }
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
        int movePower = 0;

        for(int i=0; i < battleMovesList.Length; i++)
        {
           
            if (battleMovesList[i].moveName == activeCharacters[currentTurn].AttackMovesAvailable()[selectedAttack])
            {
                movePower = PlayMove(selectedPlayerToAttack, i);
            }

        }
        DealDamage(selectedPlayerToAttack, movePower);

        UpdateStats();
    }

    private int PlayMove(int selectedPlayerToAttack, int i)
    {
        int movePower;
        Instantiate(
               battleMovesList[i].moveEffect,
               activeCharacters[selectedPlayerToAttack].transform.position,
               activeCharacters[selectedPlayerToAttack].transform.rotation);

        Instantiate(
            attackEffect,
            activeCharacters[currentTurn].transform.position,
            activeCharacters[currentTurn].transform.rotation
            );

        movePower = battleMovesList[i].movePower;
        return movePower;
    }

    private void DealDamage(int selectedCharacter, int movePower)
    {
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].wpnPower;
        float defencePower = activeCharacters[selectedCharacter].defence + activeCharacters[selectedCharacter].armorDefence;

        float damageAmount = (attackPower / defencePower) * movePower * Random.Range(0.9f, 1.1f);

        activeCharacters[selectedCharacter].TakeDamage((int)damageAmount);
    }

    public void UpdateStats()
    {
        for(int i=0; i < playersNameText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {
                if (activeCharacters[i].IsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];

                    playersNameText[i].text = playerData.characterName;

                    PlayerHealthSlider[i].maxValue = playerData.maxHp;
                    PlayerHealthSlider[i].value = playerData.currentHp;

                    PlayerManaSlider[i].maxValue = playerData.maxMana;
                    PlayerHealthSlider[i].value = playerData.currentMana;

                }
                else
                {
                    players[i].gameObject.SetActive(false);
                }

            }
            else
            {
                players[i].gameObject.SetActive(false);
            }
        }
    }

    //player attacking methods

    public void PlayerAttack(string moveName, int targetEnemy)
    {
        // int targetEnemy = 3;
        int movePower = 0;

        for(int i=0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].moveName == moveName)
            {
                movePower = PlayMove(targetEnemy, i);
            }
        }
        DealDamage(targetEnemy, movePower);
        NextTurn();
        enemyTargetPanel.SetActive(false);
    }

    public void OpenTargetMenu(string moveName)
    {
        enemyTargetPanel.SetActive(true);
        List<int> Enemies = new List<int>();
        for(int i = 0; i<activeCharacters.Count; i++) 
        {
            if (!activeCharacters[i].IsPlayer())
            {
                Enemies.Add(i);
            }
        }

        for(int i = 0; i < targetButtons.Length; i++)
        {
            if (Enemies.Count > i)
            {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattleTarget = Enemies[i];
                targetButtons[i].targetName.text = activeCharacters[Enemies[i]].characterName;
            }
        }
    }

    public void OpenMagicPanel()
    {
        magicChoicePanel.SetActive(true);

        for(int i=0; i < magicButtons.Length; i++)
        {
            if (activeCharacters[currentTurn].AttackMovesAvailable().Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = GetActiveCharacter().AttackMovesAvailable()[i];
                magicButtons[i].spellNameText.text = magicButtons[i].spellName;

                for(int j=0; j < battleMovesList.Length; j++)
                {
                    if (battleMovesList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = battleMovesList[j].manaCost;
                        magicButtons[i].spellCostText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
        }
    }

    public BattleCharacters GetActiveCharacter()
    {
        return activeCharacters[currentTurn];
    }
}
