using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemBullet : MonoBehaviour {

    public float speed;

    [HideInInspector]
    public float bulletDamage;

    public int objectID;

    public Vector2 targetLocation;

    private Vector2 bulletDirection;

    private Rigidbody2D rb;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        bulletDirection = new Vector2(targetLocation.x - transform.position.x, targetLocation.y - transform.position.y);
    }

    void FixedUpdate()
    {
        rb.velocity = bulletDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<PlayerHealth>().DealDamage(bulletDamage);
            gameObject.SetActive(false);
        }

        if(col.tag == "Soldier" && objectID != 0)
        {
            col.GetComponent<EnemHealth>().DealDamage(bulletDamage);
            gameObject.SetActive(false);
        }

        if(col.tag == "Enemy" && objectID != 1)
        {
            col.GetComponent<EnemHealth>().DealDamage(bulletDamage);
            gameObject.SetActive(false);
        }

        if(col.tag == "EnemBullet")
        {
            gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
