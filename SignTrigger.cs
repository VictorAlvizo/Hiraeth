using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignTrigger : MonoBehaviour {

    public TextClass dialog;

    private bool canRead = false;

    void TriggerDialog()
    {
        SignManager.instance.ReadSign(dialog);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && canRead)
        {
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
        canRead = false;
    }
}
