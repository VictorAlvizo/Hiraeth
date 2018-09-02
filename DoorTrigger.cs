using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour {

    public int sceneIndex;
    public int spawnIndex;

    private bool canEnter = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canEnter)
        {
            GameObject.Find("ActionPopUp").SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerControl>().pointIndex = spawnIndex;

            AudioManager.instance.PlaySound("DoorOpen");

            EliminateDuds();

            LoadScene.instance.StartLoad(sceneIndex);
        }
    }

    void EliminateDuds()
    {
        GameObject[] allEnems = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allSoldiers = GameObject.FindGameObjectsWithTag("Soldier");

        foreach(GameObject currentEnem in allEnems)
        {
            currentEnem.GetComponent<EnemHealth>().Death();
        }

        foreach(GameObject currentSoldier in allSoldiers)
        {
            currentSoldier.GetComponent<EnemHealth>().Death();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            canEnter = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            canEnter = false;
        }
    }
}
