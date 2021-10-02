using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float maxSpread = 0;
    
    public float projectilesPerShot = 1;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    // TODO:  public int magazineSize;
    // TODO:  public int ammoCapacity;
    // TODO:  public float reloadTime;
    // TODO:  public float rateOfFire; 
     public void OnFire(InputAction.CallbackContext context)
    {
        Shoot(fpsCam.transform.position, fpsCam.transform.forward);
        
        Vector3 start = fpsCam.transform.position;
        for (int i = 0; i < projectilesPerShot; i++)
        {   
            Vector3 spreadDirection = fpsCam.transform.forward;
            spreadDirection.x += Random.Range(-maxSpread, maxSpread);
            spreadDirection.y += Random.Range(-maxSpread, maxSpread);
            spreadDirection.z += Random.Range(-maxSpread, maxSpread);

            Shoot(start, spreadDirection);      
        }

        Debug.Log("Fire!");
    }
   protected void Update() {
        /*if (Input.GetButtonDown("Fire"))
        {
            Shoot(fpsCam.transform.position, fpsCam.transform.forward);
            
        }
        */
    }
   public void Shoot(Vector3 position,Vector3 direction ) {
        //Vector3 direction = fpsCam.transform.position;
        //Vector3 direction = fpsCam.transform.forward;
        RaycastHit hit;
        muzzleFlash.Play();
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