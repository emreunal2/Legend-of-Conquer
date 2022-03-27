using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

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


    // Start is called before the first frame update
    void Start()
    {
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

    public void AddXp (int amountXP)
    {
        currentXP += amountXP;
        if(currentXP > neededXP[playerLevel])
        {
            currentXP -= neededXP[playerLevel];
            playerLevel++;
        }

    }
}
