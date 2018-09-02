using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveTrigger : MonoBehaviour {

    public TextClass dialog;

    private bool canRead = false;

    void TriggerStone()
    {
        GSManager.instance.StartTablet(dialog);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canRead)
        {
            TriggerStone();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            canRead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            canRead = false;
        }
    }
}
