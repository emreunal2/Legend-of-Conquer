using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogController : MonoBehaviour
{
    public static DialogController instance;
    [SerializeField] TextMeshProUGUI dialogText, nameText;
    [SerializeField] GameObject dialogBox, nameBox;
    [SerializeField] string[] dialogSentences;
    [SerializeField] int currentSentence;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogText.text = dialogSentences[currentSentence];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                currentSentence++;
                if (currentSentence >= dialogSentences.Length)
                {
                    dialogBox.SetActive(false);
                    GameManager.instance.dialogBoxOpened = false;
                }
                else
                {
                    CheckForName();
                    dialogText.text = dialogSentences[currentSentence];
                }
            }

        }
    }

    public void activateDialog(string[] newSentencesToUse)
    {
        dialogSentences = newSentencesToUse;
        currentSentence = 0;
        CheckForName();
        dialogText.text = dialogSentences[currentSentence];
        dialogBox.SetActive(true);
        GameManager.instance.dialogBoxOpened = true;
    }

    public bool isDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }

    public void CheckForName()
    {
        if (dialogSentences[currentSentence].StartsWith("#"))
        {
            nameText.text = dialogSentences[currentSentence].Replace("#", "");
            currentSentence++;
        }
    }
}
