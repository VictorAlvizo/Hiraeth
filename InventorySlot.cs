using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;

    public Text title;
    public Text information;

    public bool activeSelected = false;

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

    public void SelectInventory()
    {
        if (ChestManager.instance.exploringChest)
        {
            bool added = ChestManager.instance.AddItem(currentItem);

            if (added)
            {
                Inventory.instance.RemoveItem(currentItem);
            }

        }else if (TradeManager.instance.tradeOpen)
        {
            bool sold = TradeManager.instance.MakePurchase(currentItem, 0);

            if (sold)
            {
                Inventory.instance.RemoveItem(currentItem);
            }
        }
        else
        {
            if (activeSelected)
            {
                activeSelected = false;
                title.text = "";
                information.text = "";
            }
            else
            {
                activeSelected = true;
                title.text = currentItem.itemName;
                information.text = currentItem.itemInformation;

                currentItem.Use();
            }
        }

        AudioManager.instance.PlaySound("Select");
    }
}
