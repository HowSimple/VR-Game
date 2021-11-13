using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Launcher : Gun
{
    //public Rigidbody projectile;
    public GameObject projectile;
   // public GameObject muzzle;
    public float initialSpeed;
    public GameObject explosionEffect;
    private void ShootProjectile()
    {
        GameObject p = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation) ;
        p.SetActive(true);
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

    void OnCollisionEnter(Collision col)
    {
        
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(explosion, 3);
    }
   
}
