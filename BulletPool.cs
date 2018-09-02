using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

    public static BulletPool instance = null;

    public List<GameObject> playerBullets = new List<GameObject>();
    public List<GameObject> enemBullets = new List<GameObject>();

    public GameObject pb;
    public GameObject eb;

    public int pbSize;
    public int ebSize;

    private int currentPBIndex = 0;
    private int currentEBIndex = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < pbSize; i++)
        {
            GameObject obj = Instantiate(pb);

            obj.SetActive(false);
            playerBullets.Add(obj);
        }

        for(int i = 0; i<ebSize; i++)
        {
            GameObject obj = Instantiate(eb);

            obj.SetActive(false);
            enemBullets.Add(obj);
        }
    }

    public void UsePBPool(Vector2 direction)
    {
        playerBullets[currentPBIndex].transform.position = direction;
        playerBullets[currentPBIndex].SetActive(true);

        currentPBIndex++;

        if(currentPBIndex >= pbSize)
        {
            currentPBIndex = 0;
        }
    }

    public void UseEBPool(Vector2 direction, Vector2 target, float bulletDamage, int objectID)
    {
        enemBullets[currentEBIndex].transform.position = direction;
        enemBullets[currentEBIndex].GetComponent<EnemBullet>().bulletDamage = bulletDamage;
        enemBullets[currentEBIndex].GetComponent<EnemBullet>().targetLocation = target;
        enemBullets[currentEBIndex].GetComponent<EnemBullet>().objectID = objectID;

        enemBullets[currentEBIndex].SetActive(true);

        currentEBIndex++;

        if(currentEBIndex >= pbSize)
        {
            currentEBIndex = 0;
        }
    }
}
