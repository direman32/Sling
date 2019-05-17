using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private LogLovel log;
    private Rigidbody2D rb;
    private bool allowedToMove;
    private bool facingRight;
    private bool colliderOn;
    private bool firstWall;
    public float moveSpeed = 1500f;
    public float maxMoveSpeed = 14;
    public float timeSinceMove = 0f;
    public float timeSpawnnedIn;
    private GameObject player;

    private void Log(string message, LogLovel.LogCategory logCategory)
    {
        Debug.Log(logCategory + " : " + message);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        allowedToMove = true;
        facingRight = true;
        firstWall = true;
        colliderOn = false;
        timeSpawnnedIn = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Log("Collided With : " + collision.collider.name, LogLovel.LogCategory.PHYSICS);
        if (colliderOn)
        {
            allowedToMove = true;
            if (collision.collider.name.Equals("WallCollider") && firstWall)
            {
                collision.rigidbody.velocity = new Vector3();
                firstWall = !firstWall;
                Log("Hit Wall", LogLovel.LogCategory.PHYSICS);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        firstWall = !firstWall;
        Log("Stopped Colliding Wall", LogLovel.LogCategory.PHYSICS);
    }

    private float getTime()
    {
        return Time.deltaTime;
    }

    private void setBodyVelocity(Vector3 vector)
    {
        rb.velocity = vector;
    }

    private Vector3 getBodyVelocity()
    {
        return rb.velocity;
    }

    void Update()
    {
        timeSpawnnedIn += getTime();
        Log("Time Since Spawn: " + timeSpawnnedIn, LogLovel.LogCategory.PHYSICS);
        if (timeSpawnnedIn < 1)
        {
            setBodyVelocity(Vector3.zero);
            Log("poo", LogLovel.LogCategory.TEST);
        }
        if (allowedToMove)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Move(1);
            }
            if (player.transform.position.x < transform.position.x)
            {
                Move(-1);
            }
            if (player.transform.position.x < transform.position.x)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(getBodyVelocity().x, moveSpeed * Time.deltaTime);
        allowedToMove = false;
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
}
