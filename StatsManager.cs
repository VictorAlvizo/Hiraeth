using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

    public static StatsManager instance = null;

    public Text[] amountText = new Text[3];

    [HideInInspector]
    public int[] statAmounts = new int[3];

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

    public void AddAmount(int amount, int index)
    {
        if(index != 0)
        {
            statAmounts[index] = amount;
        }
        else
        {
            statAmounts[index] += amount;
        }

        amountText[index].text = statAmounts[index].ToString();
    }
}
