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
    public void Start() { 
        allowFire = true; 
        gunAudioSource = GetComponent<AudioSource>();
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Shoot());

            Debug.Log("Fire!");
        }
    }
    public void Reload()
    {

        int ammo = Math.Min(carriedAmmo, magazineSize);
        carriedAmmo -= ammo;
        loadedAmmo = ammo;
    }
    private Vector3 Spread()
    {
        Vector3 spreadDirection = muzzle.transform.forward;
        spreadDirection.x += UnityEngine.Random.Range(-maxSpread, maxSpread);
        spreadDirection.y += UnityEngine.Random.Range(-maxSpread, maxSpread);
        spreadDirection.z += UnityEngine.Random.Range(-maxSpread, maxSpread);
        return spreadDirection;
    }
    
    public virtual IEnumerator Shoot()
    {
        
        if (allowFire)
        {
            
            allowFire = false;

            for (int i = 0; i < projectilesPerShot; i++)
            {
                ///ShootRay(S)
                Vector3 spreadDirection = Spread();
                RaycastHit hit;
                muzzleFlash.Play();
                gunAudioSource.volume = 0.2f;
                gunAudioSource.PlayOneShot(gunshotAudio);
                Debug.DrawRay(muzzle.transform.position, spreadDirection * range, Color.green);
                if (Physics.Raycast(muzzle.transform.position, spreadDirection, out hit, range))
                {
                    Debug.Log(hit.transform.name);

                    Health targetHealth = hit.transform.GetComponent<Health>();
                    if (targetHealth != null)
                    {
                        targetHealth.takeDamage(damage);
                    }
                    GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 2f);

                }
            }
            Debug.Log("Fire!");
            
            yield return new WaitForSeconds(rateOfFire);
            allowFire = true;
        }
        else yield return null;
        
        
    }

        
        
}