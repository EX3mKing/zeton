using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource music_source, sfx_source;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlaySFX(AudioClip clip)
    {
        sfx_source.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        music_source.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        music_source.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        sfx_source.volume = value;
    }
}
