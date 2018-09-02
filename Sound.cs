using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    public AudioClip clip;
    public string audioName;

    [Range(0, 1)]
    public float volume;

    [Range(0, 3)]
    public float pitch;

    public bool loop;
    public bool backGround;

    [HideInInspector]
    public float maxVolume;

    [HideInInspector]
    public AudioSource soundSource;
}
