using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX, backgroundMusic;
    public static AudioManager instance;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay < SFX.Length)
        {
            SFX[soundToPlay].Play();
        }
    }

    public void StopMusic()
    {
        foreach(AudioSource music in backgroundMusic)
        {
            music.Stop();
        }
    }

    public void PlayMusic(int soundToPlay)
    {
        StopMusic();
        if (soundToPlay < backgroundMusic.Length)
        {
            backgroundMusic[soundToPlay].Play();
        }
    }
}
