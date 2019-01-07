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
    [Header("input reference")]
    private float moveInput;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<CharacterCustomController>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
       
        Vector2 lookInput = new Vector2(Input.GetAxisRaw("HorizontalLook"), Input.GetAxisRaw("VerticalLook"));
        float inputAngle = Mathf.Atan2(-lookInput.x, lookInput.y) * Mathf.Rad2Deg;
        directionPointer.transform.eulerAngles = Vector3.forward * inputAngle;

        
        if (Input.GetButtonDown("FireRightBumper"))
        {
            Vector2 jumpDirection = (directionPoint.transform.position - transform.position).normalized;
            print("jumppppnowwwww" + jumpDirection);
            myController.Jump(jumpDirection);
        }
        
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        myController.Move(moveInput);
    }
}
