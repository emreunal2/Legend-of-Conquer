using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleNotification : MonoBehaviour
{
    [SerializeField] float timeAlive;
    [SerializeField] TextMeshProUGUI textNotice;
    // Start is called before the first frame update
    public void SetText(string text)
    {
        textNotice.text = text;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(Dissapaer());
    }

    IEnumerator Dissapaer()
    {
        yield return new WaitForSeconds(timeAlive);
        gameObject.SetActive(false);
    }
}
