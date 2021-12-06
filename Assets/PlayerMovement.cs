using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    private Health hp;

    private Dash dash;
    private GunController gunController;
    private CharacterController player;
    private Character character;
    private Vector2 currentMove;
    private AudioSource audio;
    public float walkSpeed = 10f;
    public float sprintModifier = 1.30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jetCharge;
    public bool jetpackOn;

    
    Vector3 velocity;
    
    public AudioClip heal;
    void OnTriggerEnter(Collider col)
    {
        //&& hp.healthPoints< hp.maxHP
        if (col.gameObject.tag == "Health" )
            {
                    hp.heal(10);
                    Destroy(col.gameObject);
                    audio.volume = 0.6f;
                    audio.PlayOneShot(heal);
            }
    }
   
    
    private void Awake()
    {
        player = this.GetComponent<CharacterController>();
        character = this.GetComponent<Character>();
        jetCharge = 1.00f;
        gunController = this.GetComponent<GunController>();
        audio = this.GetComponent<AudioSource>();
        dash = this.GetComponent<Dash>();
        hp = this.GetComponent<Health>();
        jetpackOn = false;
        //jetRechargeBar.fillAmount(100);
    }

    private void FixedUpdate()
    {
        /* if (  isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        */
        
        if (jetpackOn && jetCharge >.20f)
        {
            velocity.y += jetpackAcceleration * Time.deltaTime; 
            jetCharge-=0.01f;
                                                                                                                                                                                                                    

        }
        else
        {
            jetCharge+=0.01f;
        }
        //jetRechargeBar.fillAmount(jetCharge);     
          //healthUI.text = "Health: "  +hp.healthPoints.ToString();

        Vector3 moveDirection = transform.right * currentMove.x + transform.forward * currentMove.y;
        player.Move( moveDirection * walkSpeed * Time.deltaTime);


        Vector3 moveVelocity = walkSpeed * (
         currentMove.x * Vector3.right +
         currentMove.y * Vector3.forward
       );

        velocity.y += gravity * Time.deltaTime;
       //  transform.position += moveThisFrame;
       // player.Move(moveThisFrame* walkSpeed *Time.deltaTime);

        player.Move(velocity * Time.deltaTime);


    }
    
    public void Fire(InputAction.CallbackContext context)
    {
        
        gunController.ShootGun(character.damageModifier);
            
        Debug.Log("Fire! --controller");
    }
    public void SwitchWeapon (InputAction.CallbackContext context)
    {
        gunController.SwitchWeapon();
        audio.volume = 0.2f;
        //audio.PlayOneShot(switchSoundEffect);
        Debug.Log("Switched weapon");
    }
     public void Reload(InputAction.CallbackContext context)
    {
        
        gunController.ReloadWeapon();
        
        Debug.Log("Reloaded weapon");
    }

    
    public void DashForward(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
        

        //Vector3 destination = player.transform.forward * dashDistance;
        Vector3 destination =   transform.forward * dash.dashDistance;
        StartCoroutine(dash.DashOverTime(gameObject, destination, dash.dashDuration));





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
       
        if (context.started)
        {
             Debug.Log("Jetpack start");
            jetpackOn = true;
           // velocity.y += jetpackAcceleration * Time.deltaTime;    
           
        }
        
    
          if (context.canceled)
        {
            jetpackOn = false;
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
