using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour {

    public float speed;

    public float minD;
    public float maxD;

    public float shootingRange;

    private float waitTime;
    private float stuckTimer = 2f;

    private bool moving = false;
    private bool renewLast = false;

    private Rigidbody2D rb;

    private Animator animate;

    private Vector2 target;
    private Vector2 direction;
    private Vector2 lastPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    void Start()
    {
        waitTime = ResetTimer();
    }

    void FixedUpdate()
    {
        target = FindTarget();

        if (InRange())
        {
            if (FireRange())
            {
                animate.SetInteger("Direction", 0);

                GetComponent<EnemWeaponManager>().Fire(target, 1);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

                Vector3 heading = (Vector3)target - transform.position;

                animate.SetInteger("Direction", NewAlgorithm(transform.forward, heading, transform.up));
            }
        }
        else
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
                    waitTime = ResetTimer();
                }
            }
        }
    }

    bool InRange()
    {
        if(Vector2.Distance(transform.position, target) <= 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool FireRange()
    {
        if(Vector2.Distance(transform.position, target) <= shootingRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector2 FindTarget()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Soldier");

        GameObject playerGO = GameObject.Find("Player");

        if (allTargets.Length == 0)
        {
            return playerGO.transform.position;
        }

        float closetDistance = Mathf.Infinity;
        GameObject closetTarget = null;

        closetDistance = (playerGO.transform.position - transform.position).sqrMagnitude;
        closetTarget = playerGO;

        foreach (GameObject currentTarget in allTargets)
        {
            float distance = (currentTarget.transform.position - transform.position).sqrMagnitude;

            if (distance < closetDistance)
            {
                closetDistance = distance;
                closetTarget = currentTarget;
            }
        }

        return closetTarget.transform.position;
    }

    int NewAlgorithm(Vector3 foward, Vector3 targetDir, Vector3 up)
    {
        Vector3 prep = Vector3.Cross(foward, targetDir);
        float dir = Vector3.Dot(prep, up);

        if(dir > 0f)
        {
            return 3;

        }else if(dir < 0f)
        {
            return 4;
        }
        else
        {
            return 0;
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
                animate.SetInteger("Direction", 1);
                break;

            case 3:
                newDir = new Vector2(transform.position.x, transform.position.y + Random.Range(minD, maxD));
                animate.SetInteger("Direction", 2);
                break;

            default:
                newDir = new Vector2(transform.position.x, transform.position.y + Random.Range(minD, maxD));
                animate.SetInteger("Direction", 0);
                break;
        }

        return newDir;
    }

    float ResetTimer()
    {
        return Random.Range(1f, 3.5f);
    }
}
