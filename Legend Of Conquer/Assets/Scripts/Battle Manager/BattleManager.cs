using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;
    [SerializeField] Transform[] playerPositions, enemyPositions;
    [SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            isBattleActive = true;
            GameManager.instance.battleActive = true;
            battleScene.SetActive(true);
        }
    }
}
