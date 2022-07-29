using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    [SerializeField] float effectTime;
    [SerializeField] int SFXNumber;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(SFXNumber);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}
