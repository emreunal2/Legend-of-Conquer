using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMenager : MonoBehaviour
{
    [SerializeField] Image imageToFade;

    public static MenuMenager instance;
    // Start is called before the first frame update



    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");

    }
}
