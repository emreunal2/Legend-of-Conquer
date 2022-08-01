using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleMovesButton : MonoBehaviour
{
    public string spellName;
    public int spellCost;

    public TextMeshProUGUI spellNameText, spellCostText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (BattleManager.instance.GetActiveCharacter().currentMana >= spellCost)
        {
            BattleManager.instance.magicChoicePanel.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.GetActiveCharacter().currentMana -= spellCost;
        }
    }
}
