using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public Animator blackOut;

    public Text sfxText;
    public Text musicText;
    public Text pitchText;

    public Slider sfxSlider;
    public Slider musicSlider;
    public Slider pitchSlider;

    private Animator animate;

    private bool isActive = false;

    void Awake()
    {
        animate = GetComponent<Animator>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !isActive)
        {
            isActive = true;
            Time.timeScale = 0;

            animate.Play("PauseIn");
            blackOut.Play("BFadeIN");

            AudioManager.instance.PlaySound("UIActive");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            isActive = false;
            Time.timeScale = 1;

            animate.Play("PauseOut");
            blackOut.Play("BFadeOUT");
        }
	}

    public void QuitButton()
    {
        Application.Quit();
    }

    public void GitHubSend()
    {
        Application.OpenURL("https://github.com/VictorAlvizo");
    }

    public void SfxVolumeChange()
    {
        if(sfxSlider.value == 1)
        {
            sfxText.text = "100%";
        }
        else
        {
            float roundValue = Mathf.CeilToInt(sfxSlider.value * 100);
            sfxText.text = string.Format("{0}%", roundValue);
        }

        AudioManager.instance.SfxVolume(sfxSlider.value);
    }

    public void MusicVolumeChange()
    {
        if (sfxSlider.value == 1)
        {
            musicText.text = "100%";
        }
        else
        {
            float roundValue = Mathf.CeilToInt(musicSlider.value * 100);
            musicText.text = string.Format("{0}%", roundValue);
        }

        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void PitchChange()
    {
        if(pitchSlider.value == 3)
        {
            pitchText.text = "100%";
        }
        else
        {
            float roundValue = Mathf.CeilToInt(pitchSlider.value * 33.3f);
            pitchText.text = string.Format("{0}%", roundValue);
        }

        AudioManager.instance.PitchChange(pitchSlider.value);
    }
}
