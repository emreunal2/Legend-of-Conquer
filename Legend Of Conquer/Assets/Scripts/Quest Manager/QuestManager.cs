using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkers;
    void Start()
    {
        instance = this;
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

    public void UpdateQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsOfType<QuestObject>();

        if(questObjects.Length > 0)
        {
            foreach(QuestObject qo in questObjects)
            {
                qo.CheckForCompletion();
            }
        }
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
        UpdateQuestObjects();
    }

    public void MarkQuestInCompleted(string questToMark)
    {
        int questNumberCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberCheck] = false;
        UpdateQuestObjects();
    }

    public void SaveQuestData()
    {
        for(int i=0; i < questNames.Length; i++)
        {
            if (questMarkers[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 1);
            }

            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for(int i=0; i<questNames.Length; i++)
        {
            int valueToSet = 0;
            string keyToUse = "QuestMarker_" + questNames[i];

            if (PlayerPrefs.HasKey(keyToUse))
            {
                valueToSet = PlayerPrefs.GetInt(keyToUse);
            }

            if(valueToSet == 0)
            {
                questMarkers[i] = false;
            }
            else
            {
                questMarkers[i] = true;
            }
        }
    }
}
