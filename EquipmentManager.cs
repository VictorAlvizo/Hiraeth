using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public static EquipmentManager instance = null;

    public GameObject[] itemStorage;

    [HideInInspector]
    public Equipment[] currentEquipment;

    Inventory inventory;

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

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        if(slotIndex == 0)
        {
            WeaponManager.instance.weapon = newItem;
            StatsManager.instance.AddAmount(newItem.damageModifer, 2);
        }
        else
        {
            StatsManager.instance.AddAmount(newItem.armorModifer, 1);
        }

        if(currentEquipment[slotIndex] != null)
        {
            if(slotIndex != 0)
            {
                ChangeSprite(slotIndex);
            }

            inventory.Add(currentEquipment[slotIndex]);
        }

        currentEquipment[slotIndex] = newItem;

        if(slotIndex != 0)
        {
            ChangeSprite(slotIndex);
        }

        AudioManager.instance.PlaySound("Equip");
    }

    void ChangeSprite(int slotIndex)
    {
        int itemIndex = currentEquipment[slotIndex].itemID;

        itemStorage[itemIndex].SetActive(!itemStorage[itemIndex].activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Unequip(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Unequip(1);
        }
    }

    void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            inventory.Add(currentEquipment[slotIndex]);

            if(slotIndex == 0)
            {
                WeaponManager.instance.weapon = null;
                StatsManager.instance.AddAmount(0, 2);
            }
            else
            {
                ChangeSprite(slotIndex);
                StatsManager.instance.AddAmount(0, 1);
            }

            currentEquipment[slotIndex] = null;

            AudioManager.instance.PlaySound("Equip");
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i<currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
