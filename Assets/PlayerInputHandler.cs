using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
     public CharacterController player;
    public GameObject heldWeapon;
    public PlayerControls controls;
    public MouseLook look;

    


    public float walkSpeed = 10f;
    public float sprintModifier = 1.30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    
    float movementX;
    float movementY;



    private void Move(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
            movementX = movementVector.x;
            movementY = movementVector.y;

    }

    void Awake()
    {
        controls = new PlayerControls();
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //  rb.AddForce(movement * speed);

        Vector3 move = transform.right * movementX + transform.forward * movementY ;
         float speed = walkSpeed;   
   
       
         player.Move(move* speed *Time.deltaTime);
        

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

    }

    bool GroundCheck()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if ( isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        return isGrounded;
    }

     public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }

    public void Move(InputAction.CallbackContext context)
    {


    }
}
