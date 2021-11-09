using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
 
    //public CharacterController player;
    //public GunController gunController;
    public Health hp;

    private GunController gunController;

    private Vector2 currentMove;

    public float walkSpeed = 10f;
    public float sprintModifier = 1.30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
   //public TextMeshProUGUI healthUI;
   // public TextMeshPro healthUI;
    //public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public TMP_Text healthUI;
    Vector3 velocity;
    private void Update()
    {
        /* if (  isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        */
          healthUI.text = "Health: "  +hp.healthPoints.ToString();

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



   
   
    private CharacterController player;
    void Awake()
    {
        player = this.GetComponent<CharacterController>();
        gunController = this.GetComponent<GunController>();

    }
    public void Fire(InputAction.CallbackContext context)
    {
        
        gunController.ShootGun();
            
        Debug.Log("Fire!");
    }
    public void SwitchWeapon (InputAction.CallbackContext context)
    {
        gunController.SwitchWeapon();

        Debug.Log("Switched weapon");
    }
     public void Reload(InputAction.CallbackContext context)
    {
        
        gunController.ReloadWeapon();
        
        Debug.Log("Reloaded weapon");
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        currentMove = context.ReadValue<Vector2>();

        

    }
     public float jetpackAcceleration;
     public float jetpackMaxVelocity;
    public float jetpackDuration;
    //public ParticleSystem jetpackExhaustEffect;
     
     public void OnJetpackStart(InputAction.CallbackContext context)
    {
        //currentMove = context.ReadValue<Vector2>();
        Debug.Log("Jetpack start");
        if (context.performed)
        {
           
            velocity.y += jetpackAcceleration * Time.deltaTime;    
           
        }
        
    
          if (context.canceled)
        {
           
            //velocity.y += jetpackAcceleration * Time.deltaTime;    
            Debug.Log("Jetpack stop");
        }
        
        

    }
    
    // Start is called before the first frame update
    

    public void OnJump(InputAction.CallbackContext context)
    {
         if(isGrounded() )
         velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);



    }

   
    bool isGrounded()
    {
        //bool groundcheck = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       
        return true;
    }
   
 

}
