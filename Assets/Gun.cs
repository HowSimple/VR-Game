using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System;
public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float maxSpread = 0;
    public float projectilesPerShot = 1;

    public ParticleSystem muzzleFlash;
    public GameObject muzzle;
    public GameObject impactEffect;

    public AudioClip gunshotAudio;
    private AudioSource gunAudioSource;

    public int loadedAmmo;
    public int magazineSize;
    public int carriedAmmo;

    public float reloadTime;

    public float rateOfFire;
    public bool allowFire = true;
    public bool isHitscan;
    public void Start() { 
        allowFire = true; 
        gunAudioSource = transform.Find("Audio Source").gameObject.GetComponent<AudioSource>();
    }
 

    private Vector3 Spread()
    {
        Vector3 spreadDirection = muzzle.transform.forward;
        spreadDirection.x += UnityEngine.Random.Range(-maxSpread, maxSpread);
        spreadDirection.y += UnityEngine.Random.Range(-maxSpread, maxSpread);
        spreadDirection.z += UnityEngine.Random.Range(-maxSpread, maxSpread);
        return spreadDirection;
    }
    public virtual IEnumerator ShootGun()
    {
        yield return ShootGun(1);
    }
     private void ShootProjectile(Vector3 spreadDirection)
    {
        GameObject p = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation) ;
        p.SetActive(true);
       // p.velocity = muzzle.transform.forward * initialSpeed;
        p.GetComponent<Rigidbody>().velocity = p.transform.forward * initialSpeed;
       p.GetComponent<Rigidbody>().freezeRotation = true;
    }
    private void ShootRaycast(Vector3 direction)
    {
        RaycastHit hit;
        Debug.DrawRay(muzzle.transform.position, spreadDirection * range, Color.green);
        if (Physics.Raycast(muzzle.transform.position, spreadDirection, out hit, range))
                {
                    Debug.Log(hit.transform.name);

                    Health targetHealth = hit.transform.GetComponent<Health>();
                    if (targetHealth != null)
                    {
                        targetHealth.takeDamage(dmg);
                    }
                    GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 2f);

                }
    }
    public virtual IEnumerator ShootGun(float damageModifier)
    {
        float dmg = damage * damageModifier;
        if (allowFire)
        {
             
            allowFire = false;

            for (int i = 0; i < projectilesPerShot; i++)
            {
                Vector3 spreadDirection = Spread();
                if (isHitscan)
                    ShootRaycast(spreadDirection);
                else
                    ShootProjectile(spreadDirection);
                ///ShootRay(S)
                
                
                muzzleFlash.Play();
                gunAudioSource.volume = 0.2f;
                gunAudioSource.PlayOneShot(gunshotAudio);
                
            }
            Debug.Log("Fire!"+dmg);
            
            yield return new WaitForSeconds(rateOfFire);
            allowFire = true;
        }
        else yield return null;
        
        
    }

    public void Reload()
    {

        int ammo = Math.Min(carriedAmmo, magazineSize);
        carriedAmmo -= ammo;
        loadedAmmo = ammo;
    }
        
}