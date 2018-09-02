using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemHealthBar : MonoBehaviour {

    public float moveX;
    public float moveY;

    [HideInInspector]
    public Transform followEnemy;

    private Transform canvasTransform;

    void Awake()
    {
        canvasTransform = GameObject.Find("Canvas").transform;

        transform.SetParent(canvasTransform, false);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        Vector2 displayLocation = Camera.main.WorldToScreenPoint(followEnemy.position);
        displayLocation = new Vector2(displayLocation.x + moveX, displayLocation.y + moveY);
        transform.position = displayLocation;
    }
}
