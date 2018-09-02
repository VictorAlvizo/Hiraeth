using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    public Sound[] sound;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(Sound currentSound in sound)
        {
            currentSound.soundSource = gameObject.AddComponent<AudioSource>();
            currentSound.soundSource.clip = currentSound.clip;
            currentSound.soundSource.loop = currentSound.loop;

            currentSound.soundSource.volume = currentSound.volume;
            currentSound.soundSource.pitch = currentSound.pitch;
            currentSound.maxVolume = currentSound.volume;
        }
    }

    void Start()
    {
        PlaySound("AlyricTheme");
    }

    public void PlaySound(string audioName)
    {
        foreach(Sound searchSound in sound)
        {
            if(searchSound.audioName == audioName)
            {
                if (searchSound.backGround)
                {
                    StopBackground();
                }

                searchSound.soundSource.Play();
                break;
            }
        }
    }

    public void StopSound(string audioName)
    {
        foreach(Sound stopSound in sound)
        {
            if(stopSound.audioName == audioName)
            {
                stopSound.soundSource.Stop();
            }
        }
    }

    void StopBackground()
    {
        foreach(Sound stopSound in sound)
        {
            if (stopSound.soundSource.isPlaying && stopSound.backGround)
            {
                stopSound.soundSource.Stop();
            }
        }
    }

    public void SfxVolume(float volumeChange)
    {
        foreach(Sound changeSound in sound)
        {
            if(volumeChange != changeSound.soundSource.volume && !changeSound.backGround)
            {
                changeSound.soundSource.volume = volumeChange;
            }
        }
    }

    public void MusicVolume(float volumeChange)
    {
        foreach (Sound changeSound in sound)
        {
            if (volumeChange != changeSound.soundSource.volume && changeSound.backGround)
            {
                changeSound.soundSource.volume = volumeChange;
            }
        }
    }

    public void PitchChange(float pitchRate)
    {
        foreach(Sound adjustPitch in sound)
        {
            adjustPitch.soundSource.pitch = pitchRate;
        }
    }
}
