using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterCustomController))]
public class Player : MonoBehaviour
{
    
    [Header("movement data")]
    [Header("movement controller")]
    private CharacterCustomController myController;
    [Header("direction references")]
    public GameObject directionPointer;
    public GameObject directionPoint;
    [Header("jump references")] 
    private bool jumpTimerReset;
    [Header("input reference")]
    private float moveInput;
    
    [Header("time mechanic")]
    [Header("time reference")]
    private TimeScaleController timeScaleController;

    [Header("animations controller")] 
    [Header("player only animations reference")]
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        timeScaleController = GameObject.FindWithTag("TimeScale").GetComponent<TimeScaleController>();
        myController = GetComponent<CharacterCustomController>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
       
        //getting input from left Joystick
        Vector2 lookInput = new Vector2(Input.GetAxisRaw("HorizontalLook"), Input.GetAxisRaw("VerticalLook"));
        float inputAngle = Mathf.Atan2(-lookInput.x, lookInput.y) * Mathf.Rad2Deg;
        directionPointer.transform.eulerAngles = Vector3.forward * inputAngle;
        
        //getting input from right joystick
        Vector2 lookInput2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (lookInput2.x != 0 || lookInput2.y != 0)
        {
            float inputAngle2 = Mathf.Atan2(-lookInput2.x, lookInput2.y) * Mathf.Rad2Deg;
            directionPointer.transform.eulerAngles = Vector3.forward * inputAngle2;            
        }

        
        if (Input.GetButtonDown("FireA")) //A
        {            Vector2 jumpDirection = (directionPoint.transform.position - transform.position).normalized;
            print("jumppppnowwwww" + jumpDirection);
            myController.Jump(jumpDirection);
            jumpTimerReset = false;
        }

        if (jumpTimerReset)
        {
            timeScaleController.SetTimeScale( 1 - Input.GetAxis("FireRightTrigger") );
        }
        else
        {
            timeScaleController.SetTimeScale(1);
        }
        
        if((int)Input.GetAxis("FireRightTrigger") <= 0.5f)
        {
            jumpTimerReset = true;
        }
        
        //inicial post controllers
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("Joinha");
        }

        
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        myController.Move(moveInput);
    }
}
