using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    public string characterName;
    public int currentHp, maxHp, currentMana, maxMana, dexterity, defence, wpnPower, armorDefence;
    public bool isDead;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public string[] AttackMovesAvailable()
    {
        return attacksAvailable;
    }

    public void TakeDamage(int damageToRecieve)
    {
        currentHp -= damageToRecieve;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
    }
}
