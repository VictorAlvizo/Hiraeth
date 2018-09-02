using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour {

    public static StoreManager instance = null;

    public ListItems[] storeArray;

    #region Singleton

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton

    public List<Item> IntroduceItems(int storeIndex)
    {
        return storeArray[storeIndex].storedItems;
    }

    public void FillItem(List<Item> newItem, int storeIndex)
    {
        storeArray[storeIndex].storedItems = newItem.ToList();
    }
}
