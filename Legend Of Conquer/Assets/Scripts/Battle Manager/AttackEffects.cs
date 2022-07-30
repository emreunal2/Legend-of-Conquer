using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    [SerializeField] float effectTime;
    [SerializeField] int SFXNumber;
    [SerializeField] bool isSFXPlayed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSFXPlayed) {
            AudioManager.instance.PlaySFX(SFXNumber);
            isSFXPlayed = true;
        }
        Destroy(gameObject, effectTime);
    }
}
