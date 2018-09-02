using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth instance = null;

    public float maxHealth;

    public GameObject bloodSplatter;

    public Animator redFlash;
    public Animator damageZoom;

    public Slider healthSlider;

    public Text healthAmount;

    private float currentHealth;

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
        currentHealth = maxHealth;
        healthSlider.value = 1;
    }

    public void DealDamage(float damageAmount)
    {
        float damage = CalculateDamage(damageAmount);

        currentHealth -= damage;

        healthSlider.value = currentHealth / maxHealth;
        healthAmount.text = string.Format("{0}%", currentHealth);

        Instantiate(bloodSplatter, transform.position, Quaternion.identity);

        redFlash.Play("BloodFadeIn");
        damageZoom.Play("Zoom");

        if (currentHealth <= 0)
        {
            healthAmount.text = "0%";

            ResetProgress();
        }
    }

    public void AddHealth(float healthAdd)
    {
        if(currentHealth + healthAdd >= maxHealth)
        {
            healthSlider.value = 1;
            currentHealth = maxHealth;
            healthAmount.text = "100%";
        }
        else
        {
            currentHealth += healthAdd;

            healthSlider.value = currentHealth / maxHealth;
            healthAmount.text = string.Format("{0}%", currentHealth);
        }
    }

    float CalculateDamage(float oldDamage)
    {
        if(EquipmentManager.instance.currentEquipment[1] != null)
        {
            float damageNum = (int)oldDamage - EquipmentManager.instance.currentEquipment[1].armorModifer;

            if(damageNum <= 0)
            {
                return 0;
            }
            else
            {
                return damageNum;
            }
        }
        else
        {
            return oldDamage;
        }
    }

    void ResetProgress()
    {
        EliminateDuds();

        StatsManager.instance.AddAmount(-StatsManager.instance.statAmounts[0], 0);

        currentHealth = maxHealth;
        healthSlider.value = 1;
        healthAmount.text = "100%";

        GameObject.Find("Player").GetComponent<PlayerControl>().pointIndex = 0;
        LoadScene.instance.StartLoad(0);
    }

    void EliminateDuds()
    {
        GameObject[] allEnems = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allSoldiers = GameObject.FindGameObjectsWithTag("Soldier");

        foreach (GameObject currentEnem in allEnems)
        {
            currentEnem.GetComponent<EnemHealth>().Death();
        }

        foreach(GameObject currentSoldier in allSoldiers)
        {
            currentSoldier.GetComponent<EnemHealth>().Death();
        }
    }
}
