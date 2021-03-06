﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource scanSource;
    public AudioSource alarmSource;
    public AudioSource efxSource;

    public AudioClip femaleWtf1;
    public AudioClip femaleWtf2;
    public AudioClip femaleWtf3;
    public AudioClip maleWtf1;
    public AudioClip maleWtf2;
    public AudioClip maleWtf3;
    public AudioClip PhoneOnSound;
    public AudioClip PhoneOffSound;
    public AudioClip cashSound;

    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        musicSource.volume = 1f;
        scanSource.volume = 1f;
        alarmSource.volume = 1f;
        scanSource.mute = true;
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlaySingleRandomPitch(AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.clip = clip;
        efxSource.pitch = randomPitch;
        efxSource.Play();
    }
    
	public void ToggleScan()
	{
	    scanSource.mute = !scanSource.mute;
	}

    public void PauseMusic()
    {
        musicSource.volume = 0.2f;
        scanSource.volume = 0f;
        alarmSource.volume = 0f;
    }

    public void ContinueMusic()
    {
        musicSource.volume = 1f;
        scanSource.volume = 1f;
        alarmSource.volume = 1f;
    }

    public void StartLockdownMusic(GameManager.WTFVoice voice)
    {
        if (!alarmSource.isPlaying)
        {
            AudioClip[] clips;
            if (voice == GameManager.WTFVoice.Female)
            {
                clips = new[] { femaleWtf1, femaleWtf2, femaleWtf3 };
            }
            else
            {
                clips = new[] { maleWtf1, maleWtf2, maleWtf3 };
            }

            var clip = clips[Random.Range(0, 2)];
            
            PlaySingle(clip);
            alarmSource.PlayDelayed(clip.length);
        }
    }

    public void StopLockdownMusic()
    {
        if (alarmSource.isPlaying)
        {
            alarmSource.Stop();
        }
    }

    public void PlayPhoneSound(bool takingOut)
    {
        if (takingOut)
        {
            PlaySingle(PhoneOnSound);
        }
        else
        {
            PlaySingle(PhoneOffSound);
        }
    }

    public void PlayCashSound()
    {
        PlaySingleRandomPitch(cashSound);
    }
}
