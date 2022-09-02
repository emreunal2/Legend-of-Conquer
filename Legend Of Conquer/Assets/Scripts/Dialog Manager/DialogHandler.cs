using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public string[] sentences;
    private bool canActiveBox;
    public bool isHealer;
    public bool isUncle;
    // Start is called before the first frame update
    void Start()
    {
        canActiveBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canActiveBox && Input.GetButtonDown("Fire1") && !DialogController.instance.isDialogBoxActive())
        {
            DialogController.instance.activateDialog(sentences);
            if (isHealer)
            {
                PlayerStats.instance.currentHP = PlayerStats.instance._maxHP; 
            }

            if (isUncle)
            {
                GameManager.instance.currentCoin = 100;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActiveBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActiveBox = false;
        }
    }
}
