using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float maxSpread = 0;
    
    public float projectilesPerShot = 1;
    

    public ParticleSystem muzzleFlash;
    public GameObject muzzle;
    public GameObject impactEffect;

    public AudioClip gunshotAudio;
    public AudioSource gunAudioSource;

    // TODO:  public int magazineSize;
    // TODO:  public int ammoCapacity;
    // TODO:  public float reloadTime;
    // TODO:  public float rateOfFire; 
     public void Fire(InputAction.CallbackContext context)
    {
        //Shoot(fpsCam.transform.position, fpsCam.transform.forward);
        Shoot(transform.position, transform.forward);
        Vector3 start = transform.position;
        for (int i = 0; i < projectilesPerShot; i++)
        {   
            Vector3 spreadDirection = transform.forward;
            spreadDirection.x += Random.Range(-maxSpread, maxSpread);
            spreadDirection.y += Random.Range(-maxSpread, maxSpread);
            spreadDirection.z += Random.Range(-maxSpread, maxSpread);

            Shoot(start, spreadDirection);      
        }

        Debug.Log("Fire!");
    }
   
   public void Shoot(Vector3 position,Vector3 direction ) {
      
        RaycastHit hit;
        muzzleFlash.Play();
        gunAudioSource.volume = 0.2f;
        gunAudioSource.PlayOneShot(gunshotAudio);
        Debug.DrawRay(position, direction* range, Color.green);
        
        Debug.DrawLine(position,direction, Color.black, 10f)    ;
        if (Physics.Raycast(position, direction, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
           Health targetHealth =  hit.transform.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.takeDamage(damage);
            }
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);

        }

        
    }
}