using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public static PlayerControl instance = null;

    public float speed;

    public GameObject actionPopUp;

    public Text actionChar;
    public Text actionTitle;

    [HideInInspector]
    public int pointIndex = 0;

    [HideInInspector]
    public int bulletDirection = 0;

    private Animator animate;

    private Rigidbody2D rb;

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

        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horzMove * speed, vertMove * speed);
    }

    void Update () {

        if (Input.GetKey("a"))
        {
            bulletDirection = 4;
            animate.SetInteger("moveDir", 4);
        }

        if (Input.GetKey("d"))
        {
            bulletDirection = 3;
            animate.SetInteger("moveDir", 3);
        }

        if (Input.GetKey("w"))
        {
            bulletDirection = 2;
            animate.SetInteger("moveDir", 2);
        }

        if (Input.GetKey("s"))
        {
            bulletDirection = 1;
            animate.SetInteger("moveDir", 1);
        }

        if (Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("w")) 
        {
            rb.velocity = Vector2.zero;

            bulletDirection = 0;
            animate.SetInteger("moveDir", 0);
        }

        if (Input.GetMouseButton(0))
        {
            WeaponManager.instance.Fire();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Grave" || col.tag == "Sign")
        {
            actionPopUp.SetActive(true);
            actionChar.text = "R";
            actionTitle.text = "Read";
            actionTitle.rectTransform.anchoredPosition = new Vector3(-6.3f, 37f, actionTitle.transform.position.z);
        }
        
        if(col.tag == "Dialog")
        {
            actionPopUp.SetActive(true);
            actionChar.text = "T";
            actionTitle.text = "Talk";
            actionTitle.rectTransform.anchoredPosition = new Vector3(-6.3f, 37f, actionTitle.transform.position.z);
        }

        if(col.tag == "Door")
        {
            actionPopUp.SetActive(true);
            actionChar.text = "E";
            actionTitle.text = "Enter";
            actionTitle.rectTransform.anchoredPosition = new Vector3(-23f, 37f, actionTitle.transform.position.z);
        }

        if(col.tag == "Chest")
        {
            actionPopUp.SetActive(true);
            actionChar.text = "O";
            actionTitle.text = "Open";
            actionTitle.rectTransform.anchoredPosition = new Vector3(-6.3f, 37f, actionTitle.transform.position.z);
        }

        if(col.tag == "Trader")
        {
            actionPopUp.SetActive(true);
            actionChar.text = "T";
            actionTitle.text = "Trade";
            actionTitle.rectTransform.anchoredPosition = new Vector3(-23f, 37f, actionTitle.transform.position.z);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Grave" || col.tag == "Sign" || col.tag == "Dialog" || col.tag == "Door" || col.tag == "Chest" || col.tag == "Trader")
        {
            actionPopUp.SetActive(false);
        }
    }
}
