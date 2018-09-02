using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWander : MonoBehaviour {

    public float speed;

    public float minD;
    public float maxD;

    private float waitTime;
    private float stuckTimer = 2f;

    private bool moving = false;
    private bool paused = false;
    private bool renewLast = false;

    private Rigidbody2D rb;

    private Animator animate;

    private Vector2 direction;
    private Vector2 lastPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    void Start()
    {
        waitTime = ExtendTime();
    }

    public void NpcTalk()
    {
        animate.SetInteger("Direction", 0);
        animate.SetBool("Talking", true);

        rb.velocity = Vector2.zero;

        moving = false;
        paused = true;
    }

    public void ContinueRoute()
    {
        animate.SetBool("Talking", false);
        paused = false;
    }

    void FixedUpdate()
    {
        if (!paused)
        {
            if (moving)
            {
                if (transform.position.x == direction.x && transform.position.y == direction.y)
                {
                    rb.velocity = Vector2.zero;
                    moving = false;
                    renewLast = false;
                    stuckTimer = 2f;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);

                    if (renewLast)
                    {
                        if (Vector2.Distance(transform.position, lastPos) <= .1f)
                        {
                            rb.velocity = Vector2.zero;
                            moving = false;
                            renewLast = false;
                            direction = NewDirection();
                        }
                    }

                    stuckTimer -= Time.deltaTime;

                    if (stuckTimer <= 0)
                    {
                        lastPos = transform.position;
                        renewLast = true;
                        stuckTimer = 2f;
                    }
                }
            }
            else
            {
                waitTime -= Time.deltaTime;

                if (animate.GetInteger("Direction") != 0)
                {
                    animate.SetInteger("Direction", 0);
                }

                if (waitTime <= 0)
                {
                    moving = true;
                    direction = NewDirection();
                    waitTime = ExtendTime();
                }
            }
        }
    }

    Vector2 NewDirection()
    {
        int directionSide = Random.Range(0, 4);

        Vector2 newDir = Vector2.zero;

        switch (directionSide)
        {
            case 0:
                newDir = new Vector2(transform.position.x + Random.Range(minD, maxD), transform.position.y);
                animate.SetInteger("Direction", 3);
                break;

            case 1:
                newDir = new Vector2(transform.position.x - Random.Range(minD, maxD), transform.position.y);
                animate.SetInteger("Direction", 4);
                break;

            case 2:
                newDir = new Vector2(transform.position.x, transform.position.y - Random.Range(minD, maxD));
                animate.SetInteger("Direction", 2);
                break;

            case 3:
                newDir = new Vector2(transform.position.x, transform.position.y + Random.Range(minD, maxD));
                animate.SetInteger("Direction", 1);
                break;

            default:
                newDir = new Vector2(transform.position.x, transform.position.y + Random.Range(minD, maxD));
                animate.SetInteger("Direction", 1);
                break;
        }

        return newDir;
    }

    float ExtendTime()
    {
        return Random.Range(1f, 3.5f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rb.velocity = Vector2.zero;
        moving = false;
        direction = NewDirection();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Border")
        {
            rb.velocity = Vector2.zero;
            moving = false;
            direction = NewDirection();
        }
    }
}
