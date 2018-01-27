using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource scanSource;
    public AudioSource alarmSource;
    public AudioSource efxSource;

    public AudioClip alarmSound;
    public AudioClip whatTheMaleSound;
    public AudioClip whatTheFemaleSound;
    public AudioClip phoneOn;
    public AudioClip phoneOff;

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

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
    
	public void ToggleScan ()
	{
	    scanSource.mute = !scanSource.mute;
	}

    public void PauseMusic()
    {
        musicSource.volume = 0.2f;
        scanSource.volume = 0f;
    }

    public void ContinueMusic()
    {
        musicSource.volume = 1f;
        scanSource.volume = 1f;
    }
}
