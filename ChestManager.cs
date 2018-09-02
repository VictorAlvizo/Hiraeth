using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour {

    public static ChestManager instance = null;

    public Animator chestUIanimate;
    public Animator blackAnimate;

    ChestSlot[] slots;

    public List<Item> items = new List<Item>();

    public int maxSpace = 20;

    [HideInInspector]
    public bool exploringChest = false;

    [HideInInspector]
    public GameObject currentChest;

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

    public void OpenChest(List<Item> newItems, GameObject newChest)
    {
        Time.timeScale = 0;
        exploringChest = true;

        slots = GetComponentsInChildren<ChestSlot>();

        AnimationController(true);

        InventoryUI.instance.active = true;
        InventoryUI.instance.animate.Play("InventoryExpand");

        items = newItems.ToList();
        currentChest = newChest;

        FillChest();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && exploringChest)
        {
            Time.timeScale = 1;
            exploringChest = false;

            AnimationController(false);

            InventoryUI.instance.active = false;
            InventoryUI.instance.animate.Play("InventoryCrush");

            currentChest.GetComponent<ChestTrigger>().UpdateItems(items);
            currentChest = null;
            slots = null;

            items.Clear();
        }

        if (Input.GetKeyDown(KeyCode.A) && exploringChest)
        {
            foreach(var addAll in items.ToList())
            {
                bool added = Inventory.instance.Add(addAll);

                if (added)
                {
                    items.Remove(addAll);
                    FillChest();
                }
            }

            AudioManager.instance.PlaySound("Select");
        }
    }

    void AnimationController(bool status)
    {
        if (status)
        {
            blackAnimate.Play("BFadeIN");
            chestUIanimate.SetBool("SetActive", true);
            GameObject.Find("ActionPopUp").SetActive(false);
        }
        else
        {
            blackAnimate.Play("BFadeOUT");
            chestUIanimate.SetBool("SetActive", false);
        }
    }

    public void FillChest()
    {
        for(int i = 0; i<slots.Length; i++)
        {
            if(i < items.Count)
            {
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public bool AddItem(Item newItem)
    {
        if(items.Count >= maxSpace)
        {
            return false;
        }

        items.Add(newItem);
        FillChest();

        return true;
    }
}
