using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TradeTrigger : MonoBehaviour {

    private bool inRange = false;

    [HideInInspector]
    public List<Item> tradeItems = new List<Item>();

    public int storeIndex;

    void BeginTrade()
    {
        tradeItems = StoreManager.instance.IntroduceItems(storeIndex).ToList();
        AudioManager.instance.PlaySound("UIActive");

        TradeManager.instance.BeginTrade(tradeItems, gameObject);
    }

    public void UpdateItems(List<Item> renewList)
    {
        tradeItems = renewList.ToList();
        StoreManager.instance.FillItem(tradeItems, storeIndex);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && inRange)
        {
            BeginTrade();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        inRange = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        inRange = false;
    }
}
