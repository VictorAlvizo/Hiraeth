using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemWeaponManager : MonoBehaviour {

    public float fireRate;

    public float damageModifer;

    public GameObject bulletPrefab;

    private float lastFired;

    public void Fire(Vector2 target, int id)
    {
        if(Time.time >= lastFired + fireRate)
        {
            lastFired = Time.time;
            AudioManager.instance.PlaySound("Shoot");

            BulletPool.instance.UseEBPool(transform.position, target, damageModifer, id);
        }
    }
}
