using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignManager : MonoBehaviour {

    public static SignManager instance = null;

    public Text titleText;
    public Text informationText;

    public Animator animate;

    public Animator blackAnimate;

    private bool inDialog = false;

    #region Singleton

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
    }

    #endregion Singleton

    public void ReadSign(TextClass dialog)
    {
        Time.timeScale = 0;
        inDialog = true;

        AudioManager.instance.PlaySound("UIActive");

        animate.SetBool("SignActive", true);
        blackAnimate.Play("BFadeIN");
        GameObject.Find("ActionPopUp").SetActive(false);

        titleText.text = dialog.title;
        informationText.text = dialog.lines[0];
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && inDialog)
        {
            inDialog = false;
            blackAnimate.Play("BFadeOUT");
            animate.SetBool("SignActive", false);

            Time.timeScale = 1;
        }
    }
}
