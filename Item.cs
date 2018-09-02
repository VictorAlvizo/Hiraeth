using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    public string itemName;
    public string itemInformation;
    public int itemID;
    public int itemPrice;
    public Sprite icon;

    public virtual void Use()
    {
        //Override
    }

    public void RemoveInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}
