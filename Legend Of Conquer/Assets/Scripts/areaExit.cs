using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class areaExit : MonoBehaviour
{
    [SerializeField] string transitionNameArea;
    [SerializeField] string sceneToLoad;
    [SerializeField] areaEnter theAreaEnter;
    // Start is called before the first frame update
    void Start()
    {
        theAreaEnter.transitionAreaName = transitionNameArea;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.transitionName = transitionNameArea;
            MenuMenager.instance.FadeImage();
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}
