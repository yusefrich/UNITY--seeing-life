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
    private float jumpTime = .5f;
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
    private Rigidbody2D rb;
    
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

        if (moveInput != 0 && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        if(isGrounded && Time.time > nextJumpTime)
        {
            float velocityX = Mathf.SmoothDamp(rb.velocity.x, 0, ref velocitySmoothX, velocitySmoothTime);
            float velocityY = Mathf.SmoothDamp(rb.velocity.y, 0, ref velocitySmoothY, velocitySmoothTime);
            
            rb.velocity = new Vector2(velocityX, velocityY);
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
