using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public static DialogManager instance = null;

    public Text titleText;
    public Text dialogText;

    public Animator animate;

    [HideInInspector]
    public GameObject npcFocus;

    private Queue<string> lines = new Queue<string>();

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

    public void StartDialog(TextClass dialog)
    {
        inDialog = true;

        animate.SetBool("DialogActive", true);
        GameObject.Find("ActionPopUp").SetActive(false);

        titleText.text = dialog.title;
        lines.Clear();

        foreach(string sentence in dialog.lines)
        {
            lines.Enqueue(sentence);
        }

        DisplaySentence();
    }

    public void OutofRange()
    {
        EndDialog();
    }

    void DisplaySentence()
    {
        if(lines.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = lines.Dequeue();

        AudioManager.instance.PlaySound("Talk");

        StopAllCoroutines();
        StartCoroutine(TypeCharacter(sentence));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inDialog)
        {
            AudioManager.instance.PlaySound("Confirm");
            DisplaySentence();
        }
    }

    IEnumerator TypeCharacter(string sentence)
    {
        dialogText.text = "";

        foreach(char letter in sentence)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void EndDialog()
    {
        AudioManager.instance.StopSound("Talk");

        inDialog = false;
        npcFocus.GetComponentInParent<NPCWander>().ContinueRoute();
        animate.SetBool("DialogActive", false);
    }
}
