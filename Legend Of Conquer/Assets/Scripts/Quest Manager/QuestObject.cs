using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    [SerializeField] string questToCheck;
    [SerializeField] bool activateIfComplete;

    public void CheckForCompletion()
    {
        if (QuestManager.instance.CheckQuestCompleted(questToCheck))
        {
            objectToActivate.SetActive(activateIfComplete);
        }
    }
}
