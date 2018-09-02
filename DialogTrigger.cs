using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public TextClass dialog;

    private bool talking = false;

    private bool canRead = false;

    void TriggerDialog()
    {
        GetComponentInParent<NPCWander>().NpcTalk();

        DialogManager.instance.npcFocus = gameObject;
        DialogManager.instance.StartDialog(dialog);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && canRead)
        {
            talking = true;
            TriggerDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            canRead = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (talking)
            {
                DialogManager.instance.OutofRange();
            }

            talking = false;
            canRead = false;
        }
    }
}
