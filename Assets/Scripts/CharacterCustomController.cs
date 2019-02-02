using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomController : MonoBehaviour
{
    
    //side movement
    [Header("side movementation")]
    public float speed;
    public float velocitySmoothTime;
    public float jumpForce;
    public float jumpTime = .5f;
    private float nextJumpTime;
    private float velocitySmoothY;
    private float velocitySmoothX;
    
    //jump
    [Header("jumping")]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumpsValue;
    private int extraJumps;

    [Header("physics body")] 
    public float gravity;
    private Rigidbody2D rb;

    [Header("animation reference")] 
    public GameObject myGraphics;

    
    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveInput)
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        bool jumping = !(Time.time > nextJumpTime);

        if (jumping)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
        }
        
        if (moveInput != 0 && !jumping)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        //this smooths the velocity of the player gradually to 0
        if (isGrounded && !jumping)
        {
            float velocityX = Mathf.SmoothDamp(rb.velocity.x, 0, ref velocitySmoothX, velocitySmoothTime);
            float velocityY = Mathf.SmoothDamp(rb.velocity.y, 0, ref velocitySmoothY, velocitySmoothTime);
            rb.velocity = new Vector2(velocityX, velocityY);
        }
        

        //setting the animations
        if ((int)rb.velocity.x != 0)
        {
            myGraphics.GetComponent<Animator>().SetFloat("current_x_direction", Mathf.Sign(rb.velocity.x));
            myGraphics.GetComponent<Animator>().SetBool("is_moving", true);
        }
        else
        {
            myGraphics.GetComponent<Animator>().SetBool("is_moving", false);
        }
        myGraphics.GetComponent<Animator>().SetFloat("current_x_velocity", rb.velocity.x);
        
        //move debug
        if (GetComponent<MovementDebugData>() != null)
        {
            GetComponent<MovementDebugData>().SetCurrentTime(Time.time);
            GetComponent<MovementDebugData>().SetNextJump(nextJumpTime);
        }
        else
        {
            Debug.Log("no debug atatch");
        }
    }

    public void Jump(Vector2 jumpDirection)
    {
        if (extraJumps > 0)
        {
            rb.velocity = jumpDirection * jumpForce;
            nextJumpTime = Time.time + jumpTime;

            extraJumps--;
        }else if (extraJumps == 0 && isGrounded)
        {
            rb.velocity = jumpDirection * jumpForce;  
            nextJumpTime = Time.time + jumpTime;

        }
    }
}
