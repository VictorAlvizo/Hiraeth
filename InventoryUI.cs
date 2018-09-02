using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public static InventoryUI instance = null;

    Inventory inventory;

    public Animator animate;

    InventorySlot[] slots;

    [HideInInspector]
    public bool active = false;

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

    void Start () {
        inventory = Inventory.instance;

        slots = GetComponentsInChildren<InventorySlot>();
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!active)
            {
                active = true;
                animate.Play("InventoryExpand");

            }
            else
            {
                active = false;
                animate.Play("InventoryCrush");
            }
        }

        if(Input.GetKeyDown(KeyCode.D) && ChestManager.instance.exploringChest)
        {
            foreach(var addAll in inventory.items.ToList())
            {
                bool added = ChestManager.instance.AddItem(addAll);

                if (added)
                {
                    inventory.RemoveItem(addAll);
                }
            }

            AudioManager.instance.PlaySound("Select");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach(InventorySlot searchSlot in slots)
            {
                if(searchSlot.currentItem.name == "BandAid")
                {
                    searchSlot.currentItem.Use();
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach(InventorySlot searchSlot in slots)
            {
                if(searchSlot.currentItem.name == "HealthPotion")
                {
                    searchSlot.currentItem.Use();
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            foreach(InventorySlot searchSlot in slots)
            {
                if(searchSlot.currentItem.name == "Medkit")
                {
                    searchSlot.currentItem.Use();
                    break;
                }
            }
        }
	}

    public void UpdateUI()
    {
        for(int i = 0; i<slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
