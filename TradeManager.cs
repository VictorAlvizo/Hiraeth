using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour {

    public static TradeManager instance = null;

    public List<Item> items = new List<Item>();

    public Text priceText;

    public int maxSpace;

    [HideInInspector]
    public bool tradeOpen = false;

    [HideInInspector]
    public GameObject currentMerchant;

    public Animator fadeAnimate;
    public Animator tradeUI;

    TradeSlot[] slots;

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

    public void BeginTrade(List<Item> newItems, GameObject newMerchant)
    {
        Time.timeScale = 0;
        tradeOpen = true;

        slots = GetComponentsInChildren<TradeSlot>();

        AnimationControl(true);

        InventoryUI.instance.active = true;
        InventoryUI.instance.animate.Play("InventoryExpand");

        items = newItems.ToList();
        currentMerchant = newMerchant;

        UpdateTrade();
    }
    
    void AnimationControl(bool status)
    {
        if (status)
        {
            fadeAnimate.Play("BFadeIN");
            tradeUI.SetBool("SetActive", true);
            GameObject.Find("ActionPopUp").SetActive(false);

            TextControl(0, 0);

            priceText.gameObject.SetActive(true);
        }
        else
        {
            fadeAnimate.Play("BFadeOUT");
            tradeUI.SetBool("SetActive", false);

            TextControl(0, 0);

            priceText.gameObject.SetActive(false);
        }
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && tradeOpen)
        {
            Time.timeScale = 1;
            tradeOpen = false;

            AnimationControl(false);

            InventoryUI.instance.active = false;
            InventoryUI.instance.animate.Play("InventoryCrush");

            currentMerchant.GetComponent<TradeTrigger>().UpdateItems(items);
            currentMerchant = null;
            slots = null;

            items.Clear();
        }
	}

    void UpdateTrade()
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
        UpdateTrade();

        return true;
    }

    public void RemoveItem(Item removeItem)
    {
        items.Remove(removeItem);
        UpdateTrade();
    }

    public bool MakePurchase(Item tradeItem, int option)
    {
        if(option == 0)
        {
            bool added = AddItem(tradeItem);

            if (added)
            {
                AudioManager.instance.PlaySound("Bought");

                StatsManager.instance.AddAmount(tradeItem.itemPrice, 0);
                TextControl(1, tradeItem.itemPrice);

                return true;
            }

            return false;
        }
        else
        {
            if(StatsManager.instance.statAmounts[0] >= tradeItem.itemPrice)
            {
                bool added = Inventory.instance.Add(tradeItem);

                if (added)
                {
                    AudioManager.instance.PlaySound("Bought");

                    StatsManager.instance.AddAmount(-tradeItem.itemPrice, 0);
                    TextControl(2, tradeItem.itemPrice);

                    return true;
                }

                return false;
            }
        }

        TextControl(3, 0);
        return false;
    }

    public void TextControl(int messageCode, int amount)
    {
        switch (messageCode)
        {
            case 0:
                priceText.text = "Trade";
                priceText.color = Color.white;
                priceText.rectTransform.anchoredPosition = new Vector3(-89f, 43, priceText.transform.position.z);
                break;

            case 1:
                priceText.text = string.Format("Sold For: {0}", amount);
                priceText.color = Color.green;
                priceText.rectTransform.anchoredPosition = new Vector3(-254f, 43, priceText.transform.position.z);
                break;

            case 2:
                priceText.text = string.Format("Bought: {0}", amount);
                priceText.color = Color.blue;
                priceText.rectTransform.anchoredPosition = new Vector3(-211f, 43, priceText.transform.position.z);
                break;

            case 3:
                priceText.text = "Not Enough";
                priceText.color = Color.red;
                priceText.rectTransform.anchoredPosition = new Vector3(-179f, 43, priceText.transform.position.z);
                break;

            default:
                priceText.text = "Trade";
                priceText.color = Color.white;
                priceText.rectTransform.anchoredPosition = new Vector3(-89f, 43, priceText.transform.position.z);
                break;
        }
    }
}
