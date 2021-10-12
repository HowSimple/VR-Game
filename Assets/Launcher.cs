using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Launcher : Gun
{
    public Rigidbody projectile;
   // public GameObject muzzle;
    public float initialSpeed;
    private void ShootProjectile()
    {
        Rigidbody p = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation) ;
       // p.velocity = muzzle.transform.forward * initialSpeed;
        p.GetComponent<Rigidbody>().velocity = p.transform.forward * initialSpeed;
       p.GetComponent<Rigidbody>().freezeRotation = true;
    }
    public void SecondaryFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //StartCoroutine(Shoot());
            ShootProjectile();
            Debug.Log("Fire projectile!");
        }
    }
    /* override public IEnumerator Shoot()
     {
         if (allowFire)
         {


         }
     }*/
}
