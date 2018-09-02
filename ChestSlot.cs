using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour {

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

    public void GainItem()
    {
        bool added = Inventory.instance.Add(currentItem);

        if (added)
        {
            ChestManager.instance.items.Remove(currentItem);
            ChestManager.instance.FillChest();
        }

        AudioManager.instance.PlaySound("Select");
    }
}
