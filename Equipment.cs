using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;

    public int damageModifer;
    public int armorModifer;

    public float fireRate;

    public override void Use()
    {
        EquipmentManager.instance.Equip(this);
        RemoveInventory();
        base.Use();
    }
}

public enum EquipmentSlot
{
    Weapon, Clothing
}