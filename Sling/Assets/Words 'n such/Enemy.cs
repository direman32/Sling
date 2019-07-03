using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 700f;
    [SerializeField]
    private float senseRadius = 8f;

    protected LogLovel log;
    protected Rigidbody2D rb;
    protected bool allowedToMove;
    protected bool facingRight;
    protected bool colliderOn;
    protected bool firstWall;
    protected bool seenPlayer;
    protected bool follow;
    public float moveSpeed;
    public float maxMoveSpeed = 14;
    public float timeSinceMove = 0f;
    public float timeSpawnnedIn;
    protected GameObject player;
    protected manager manager;
    protected CircleCollider2D senseRadiusCollider;
    private Player playerScript;
    protected int lastdir;
    protected bool playerTouch;

    protected void Log(string message, LogLovel.LogCategory logCategory)
    {
        //log.Log(message, logCategory);
    }

    public Enemy()
    {
        allowedToMove = false;
        seenPlayer = false;
        facingRight = true;
        firstWall = true;
        colliderOn = true;
        follow = true;
        timeSpawnnedIn = 0f;
        lastdir = 1;
        playerTouch = false;
        log = new LogLovel();
    }

    protected void notFollow()
    {
        follow = false;
    }

    private void Awake()
    {
        AwakeElements();
    }

    protected void AwakeElements()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
        senseRadiusCollider = gameObject.GetComponentInChildren<CircleCollider2D>();
        senseRadiusCollider.radius = senseRadius;
        moveSpeed = Random.Range(speed - 4, speed);
    }

    #region Collision and Triggers

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colName = collision.collider.name;
        Collider2D collider = gameObject.GetComponentInChildren<BoxCollider2D>();

        if (colName.Equals(collider.name))
            Physics2D.IgnoreCollision(collider, collision.collider);

        Log("Collided With : " + collision.collider.name, LogLovel.LogCategory.PHYSICS);
        if (colliderOn)
        {
            if (collision.collider.name.Equals("WallCollider") && firstWall)
            {
                 rb.velocity = Vector3.zero;
                 firstWall = !firstWall;
                 allowedToMove = false;
            }
            else if(collision.collider.name.Equals("Player"))
            {
                playerScript.TakeDamage(20);
                playerTouch = true;
            }
            else
            {
                allowedToMove = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        firstWall = !firstWall;
        Log("Stopped Colliding Wall", LogLovel.LogCategory.PHYSICS);
        playerTouch = false;

        //allowedToMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(colliderOn)
        {
            //Debug.Log(follow);
            if (collision.name == "Player" && follow)
            {
                allowedToMove = true;
                seenPlayer = true;
            }
            else if(collision.name == "Player")
            {
                seenPlayer = true;
                allowedToMove = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliderOn)
        {
            if (collision.name == "Player" && !follow)
            {
                allowedToMove = true;
            }
        }
    }

    #endregion

    protected float getTime()
    {
        return Time.deltaTime;
    }

    protected void setBodyVelocity(Vector3 vector)
    {
        rb.velocity = vector;
    }

    private Vector3 getBodyVelocity()
    {
        return rb.velocity;
    }

    void Update()
    {
        if (!manager.gameOver)
        {
            timeSpawnnedIn += getTime();
            if (timeSpawnnedIn < 1)
            {
                setBodyVelocity(Vector3.zero);
            }
            if (allowedToMove)
            {
                MovementUpdates();
            }
        }
    }

    #region Movements

    protected void MovementUpdates()
    {
        if (allowedToMove)
        {
            if (player.transform.position.x > transform.position.x)
            {
                lastdir = 1;
                Move(lastdir);
            }
            if (player.transform.position.x < transform.position.x)
            {
                lastdir = -1;
                Move(lastdir);
            }
            if (playerTouch)//Mathf.Abs(player.transform.position.x - transform.position.x) <= 1 )
            {
                //Change this to be after they hit
                Jump(lastdir);
            }
        }
    }

    protected void Jump(int dir)
    {
        rb.AddForce(new Vector2(20f * -dir, 20f), ForceMode2D.Impulse);
        allowedToMove = false;
        follow = false;
    }

    protected void Move(int dir)
    {
        //flip player
        FlipActor(dir);

        float xVelocity = getBodyVelocity().x;
        if (xVelocity < maxMoveSpeed && dir > 0)
        {
            setBodyVelocity(new Vector2(moveSpeed * getTime() * dir, getBodyVelocity().y));
        }
        else if (xVelocity > -maxMoveSpeed && dir < 0)
        {
            setBodyVelocity(new Vector2(moveSpeed * getTime() * dir, getBodyVelocity().y));
        }

        //If player is turning around, help turn faster
        if (xVelocity > 0.2f && dir < 0)
        {
            rb.AddForce(moveSpeed * 3.2f * getTime() * -Vector2.right);
        }
        if (xVelocity < 0.2f && dir > 0)
        {
            rb.AddForce(moveSpeed * 3.2f * getTime() * Vector2.right);
        }
    }

    private void FlipActor(int dir)
    {
        if ((facingRight && dir < 0) || !facingRight && dir > 0)
        {
            facingRight = !facingRight;
            if(transform.eulerAngles == new Vector3(0, 0, 0))
                transform.eulerAngles = new Vector3(0, 180, 0);
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    #endregion
}
