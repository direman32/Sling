using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float maxJumpSpeed = 200f;
    [SerializeField]
    private float timeForJump = .8f;

    private float timeSinceJump = 0f;
    private bool jumping;
    private bool falling;
    private bool grabbing;
    private bool doubleJump;
    private float jumpForce;
    private float speed;
    private float gravityScale;
    private Vector3 startOfJump;
    private Vector3 maxHeight;
    private string lastCollidedWith;

    private void Awake()
    {
        jumping = false;
        falling = false;
        doubleJump = false;
        jumpForce = maxJumpSpeed;
        speed = maxJumpSpeed;
        //Gets initial gravity scale so it can be set back to normal after jump
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        lastCollidedWith = "";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastCollidedWith = collision.collider.name;
        if ((jumping && !falling) || lastCollidedWith.Equals("Bottom"))
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        jumping = false;
        falling = false;
        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        //Debug.Log("x at land " + transform.position.x);
    }

    void Update()
    {
        movePlayer();
        checkJump();
        if (Input.GetKey("space"))
        {
            if (!jumping)
            {
                if (lastCollidedWith.Equals("WallCollider"))
                    jump();
                else if (Input.GetKeyDown("space"))
                    jump();
            }
        }
    }

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
        Vector3 movement = new Vector3(transformStuff.x + _sideWays, transformStuff.y, transformStuff.z);
        transform.position = Vector3.Lerp(transformStuff, movement, Time.deltaTime * speed);
    }

    private void jump()
    {
        if ((!jumping && !falling))
        {
            //Dashing up for the jump
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce / 8), ForceMode2D.Impulse);
            }
        }

        if (jumping && maxHeight.y < transformStuff.y)
        {
            maxHeight = new Vector3(transformStuff.x, transformStuff.y, transformStuff.z);
        }
        else if(!falling && jumping && maxHeight.y > transformStuff.y)
        {
            //Scales gravity so when falling gravity increases to make you fall faster
            GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale * 6f;
            jumping = false;
            falling = true;
            //Debug.Log("x at peak" + transform.position.x);
        }
    }
    #endregion
}
