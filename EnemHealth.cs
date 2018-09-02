using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemHealth : MonoBehaviour {

    public float maxHealth;

    public string characterName;

    public GameObject bloodSplatter;

    public GameObject healthSlider;
    public GameObject enemName;

    public Color uiColor;

    private float currentHealth;

    private GameObject trackSlider;
    private GameObject trackText;

    private Slider healthBar;

    void Start () {
        currentHealth = maxHealth;

        trackSlider = Instantiate(healthSlider, transform.position, Quaternion.identity);
        trackText = Instantiate(enemName, transform.position, Quaternion.identity);

        trackText.GetComponent<Text>().text = characterName;

        trackText.GetComponent<Text>().color = uiColor;

        trackSlider.GetComponent<EnemHealthBar>().followEnemy = transform;
        trackText.GetComponent<EnemHealthBar>().followEnemy = transform;

        healthBar = trackSlider.GetComponent<Slider>();
	}

    public void DealDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        healthBar.value = currentHealth / maxHealth;

        Instantiate(bloodSplatter, transform.position, Quaternion.identity);

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        trackSlider.GetComponent<EnemHealthBar>().Dead();
        trackText.GetComponent<EnemHealthBar>().Dead();
        Destroy(gameObject);
    }
}
