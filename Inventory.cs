using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;

    public List<Item> items = new List<Item>();

    public int maxSpace = 20;

    #region Singleton

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton

    public bool Add(Item newItem)
    {
        if(items.Count >= maxSpace)
        {
            return false;
        }

        items.Add(newItem);

        InventoryUI.instance.UpdateUI();

        return true;
    }

    public void RemoveItem(Item removeItem)
    {
        items.Remove(removeItem);
        InventoryUI.instance.UpdateUI();
    }
}
