using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour {

    public Image icon;

    public Item currentItem;

    public void AddItem(Item newItem)
    {
        currentItem = newItem;
        icon.sprite = currentItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void ClickSlot()
    {
        bool sold = TradeManager.instance.MakePurchase(currentItem, 1);

        if (sold)
        {
            TradeManager.instance.RemoveItem(currentItem);
        }

        AudioManager.instance.PlaySound("Select");
    }
}
