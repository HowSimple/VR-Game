using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
 
    public CharacterController player;
    
   // private InputAction movement;
    private Vector2 currentMove;

    public float walkSpeed = 10f;
    public float sprintModifier = 1.30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
   

    public void OnMove(InputAction.CallbackContext context)
    {
        currentMove = context.ReadValue<Vector2>();

        

    }

    // Start is called before the first frame update
    

    public void OnJump(InputAction.CallbackContext context)
    {
         if(isGrounded() )
         velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);



    }

   
    bool isGrounded()
    {
        bool groundcheck = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       
        return groundcheck;
    }
    private void Update()
    {
         if (  isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        
        Vector3 moveDirection = transform.right * currentMove.x + transform.forward * currentMove.y;
        player.Move( moveDirection * walkSpeed * Time.deltaTime);


         Vector3 moveVelocity = walkSpeed * (
          currentMove.x * Vector3.right +
          currentMove.y * Vector3.forward
        );
       // Vector3 moveThisFrame = Time.deltaTime * moveVelocity;


        velocity.y += gravity * Time.deltaTime;
       //  transform.position += moveThisFrame;
       // player.Move(moveThisFrame* walkSpeed *Time.deltaTime);

        player.Move(velocity * Time.deltaTime);


    }

    /*void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if ( isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");


         Vector3 move = transform.right * movement.x + transform.forward * movement.y;
         float speed = walkSpeed;   
   
       
         player.Move(move* speed *Time.deltaTime);
       
        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);


    }


*/

   

}
