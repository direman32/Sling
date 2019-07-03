using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables
    [SerializeField]
    private Player player;
    [SerializeField]
    private float maxSpeed = 12f;
    [SerializeField]
    private float maxJumpSpeed = 10f;
    [SerializeField]
    private float timeForJump = .8f;

    private bool jumping;
    private bool falling;
    private bool grabbing;
    private bool doubleJump;
    private float jumpForce;
    private float speed;
    private float timeSinceJump = 0f;
    private float gravityScale;
    private Vector3 startOfJump;
    private Vector3 maxHeight;
    private string lastCollidedWith;
    private manager manager;
    private Rigidbody2D rb;
    //lastDirection left is false right is true
    private bool lastDirection;
    #endregion

    private void Awake()
    {
        jumping = false;
        falling = false;
        doubleJump = false;
        lastDirection = false;
        jumpForce = maxJumpSpeed;
        speed = maxJumpSpeed;
        //Gets initial gravity scale so it can be set back to normal after jump
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        lastCollidedWith = "";
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<manager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastCollidedWith = collision.collider.name;
        if ((jumping && !falling) || lastCollidedWith.Equals("Bottom"))
            rb.velocity = Vector2.zero;
        jumping = false;
        falling = false;
        rb.gravityScale = gravityScale;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (lastCollidedWith == "WallCollider")
        {
            jumping = true;
            falling = true;
        }
    }

    void Update()
    {
        if (!manager.gameOver)
        {
            movePlayer();
            checkJump();
            if (Input.GetKey("space"))
            {
                if (!jumping)
                {
                    if (Input.GetKeyDown("space"))
                        jump();
                }
            }
        }
    }

    #region Weapon

    #endregion

    #region Movement
    private void movePlayer()
    {
        //If statement to slow playerspeed while jumping
        if (jumping)
            speed = maxSpeed / 1.5f;
        else if (falling)
            speed = maxSpeed / 2;
        else
            speed = maxSpeed;
        Vector3 transformStuff = transform.position;
        float _sideWays = Input.GetAxisRaw("Horizontal");
        directionStuff(_sideWays);
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, speed * _sideWays, speed), rb.velocity.y);
    }

    private void directionStuff(float direction)
    {
        if(direction == 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (direction > 0 && !lastDirection)
        {
            lastDirection = !lastDirection;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (direction < 0 && lastDirection)
        {
            lastDirection = !lastDirection;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void jump()
    {
        if ((!jumping && !falling))
        {
            //Dashing up for the jump
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            maxHeight = startOfJump = transform.position;
            jumping = true;
            falling = false;
            doubleJump = false;
            timeSinceJump = 0f;
            //Debug.Log("x at jump " + transform.position.x);
        }
    }

    private void checkJump()
    {
        Vector3 transformStuff = transform.position;

        if(jumping)
        {
            timeSinceJump += Time.deltaTime;
            if (timeSinceJump >= timeForJump)
            {
                //Can be used as a dash
                rb.AddForce(new Vector2(0f, -jumpForce / 8), ForceMode2D.Impulse);
            }
            if (timeSinceJump >= timeForJump && falling)
            {
                jumping = false;
                falling = false;
            }
        }

        if (jumping && maxHeight.y < transformStuff.y)
        {
            maxHeight = new Vector3(transformStuff.x, transformStuff.y, transformStuff.z);
        }
        else if(!falling && jumping && maxHeight.y > transformStuff.y)
        {
            //Scales gravity so when falling gravity increases to make you fall faster
            rb.gravityScale = rb.gravityScale * 3f;
            jumping = false;
            falling = true;
            //Debug.Log("x at peak" + transform.position.x);
        }
    }
    #endregion
}
