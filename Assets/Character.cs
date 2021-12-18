using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GunController gunController;
    public CharacterController characterController;
    public Health hp;

    public Animator mAnimator ;
    public float walkSpeed = 10f;
    public float sprintModifier = 1.30f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float viewDistance = 50f;
    public float maxViewAngle = 60f;

    public float turnSpeed = 500f;
    
   
    public Transform headPosition;
    public Vector3 velocity;
    //[HideInInspector]
    public float damageModifier = 1, defenseModifier = 1, speedModifier = 1;
    public void Start()
    {
        //damageModifier = 1;
        //defenseModifier = 1;
        //speedModifier = 1;
    }
      public AudioClip hurtSound;
      public AudioClip hitSound;
    void OnTriggerEnter(Collider col)
    {
        //&& hp.healthPoints< hp.maxHP
        if (col.gameObject.tag == "Rocket" )
            {
                    hp.takeDamage(5);
                    Destroy(col.gameObject);
                    GetComponent<AudioSource>().volume = 0.6f;
                    GetComponent<AudioSource>().PlayOneShot(hurtSound);
            }
    }
    public void Attack()
    {
        gunController.ShootGun(damageModifier);

    }
    public void addUpgrade(float damage, float defense, float speed)
    {
        damageModifier+= damage;
        defenseModifier+= defense;
        speedModifier+= speed;
    }
    public void Move(float speed)
    {
        mAnimator.SetFloat("PosZ", walkSpeed);
        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        velocity = forward * speed;
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        mAnimator.SetFloat("PosX", velocity.x);
        mAnimator.SetFloat("PosZ", velocity.z);

    }

    public bool MoveTowards(Vector3 destination)
    {
        if(true)
        {
            Vector3 pos = transform.position;

            Vector3 currentDirection = transform.forward;

            Vector3 desiredDirection = (destination - pos).normalized;
            Vector3 forward = Vector3.Scale(desiredDirection, new Vector3(1, 0, 1)).normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), turnSpeed * Time.deltaTime);
            Move(walkSpeed);
        }
        return true;
    }
  

}
