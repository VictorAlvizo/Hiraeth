using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour {

    public Animator animate;
    
    [HideInInspector]
    public List<Item> chestItems = new List<Item>();

    public int storeIndex;

    private bool canOpen = false;

    void OpenChest()
    {
        chestItems = StoreManager.instance.IntroduceItems(storeIndex).ToList();
        AudioManager.instance.PlaySound("UIActive");

        animate.SetBool("chestOpen", true);
        ChestManager.instance.OpenChest(chestItems, gameObject);
    }

    public void UpdateItems(List<Item> renewList)
    {
        chestItems = renewList.ToList();
        StoreManager.instance.FillItem(chestItems, storeIndex);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) && canOpen)
        {
            OpenChest();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        animate.SetBool("chestOpen", false);
        canOpen = false;
    }
}
