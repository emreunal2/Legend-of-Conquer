using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkers;
    void Start()
    {
        questMarkers = new bool[questNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }
        Debug.LogWarning("Quest: " + questToFind + " does not exits");
        return 0;
    }

    public bool CheckQuestCompleted(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);
        if (questNumberToCheck != 0)
        {
            return questMarkers[questNumberToCheck];
        }

        return false;
    }

    public void MarkQuestCompleted(string questToMark)
    {
        int questNumberCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberCheck] = true;
    }

    public void MarkQuestCompleted(string questToMark)
    {
        int questNumberCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberCheck] = false;
    }
}
