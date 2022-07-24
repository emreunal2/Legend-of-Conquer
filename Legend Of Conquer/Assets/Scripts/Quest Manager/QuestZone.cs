using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone : MonoBehaviour
{
    [SerializeField] string questToMark;
    [SerializeField] bool markAsCompleted;

    [SerializeField] bool markOnEnter;
    private bool canMark;

    public void MarkTheQuest()
    {
        if (markAsCompleted)
        {
            QuestManager.instance.MarkQuestCompleted(questToMark);
        }
        else
        {
            QuestManager.instance.MarkQuestInCompleted(questToMark);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(markOnEnter)
            {
                MarkTheQuest();
            }
        }
    }

}
