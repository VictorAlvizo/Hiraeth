using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {

    public ConsumableUse itemUse;

    public int coinAmount;
    public int healthAmount;

    public override void Use()
    {
        if(itemUse.ToString() == "Health")
        {
            MoreHealth();
        }
        else
        {
            MoreMoney();
        }

        RemoveInventory();
        base.Use();
    }

    void MoreHealth()
    {
        AudioManager.instance.PlaySound("MoreHealth");
        PlayerHealth.instance.AddHealth(healthAmount);
    }

    void MoreMoney()
    {
        AudioManager.instance.PlaySound("Coin");
        StatsManager.instance.AddAmount(coinAmount, 0);
    }
}

public enum ConsumableUse
{
    Health, Money
}
