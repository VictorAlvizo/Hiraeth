using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public static WeaponManager instance = null;

    public GameObject bulletPrefab;

    [HideInInspector]
    public Equipment weapon;

    private Transform playerLocation;

    private float lastShot;

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

        playerLocation = GameObject.Find("Player").transform;
    }

    public void Fire()
    {
        if(weapon != null)
        {
            if(Time.time >= lastShot + weapon.fireRate)
            {
                lastShot = Time.time;
                AudioManager.instance.PlaySound("Shoot");

                BulletPool.instance.UsePBPool(playerLocation.transform.position);
            }
        }
    }
}
