using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
   
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    // TODO:  public int magazineSize;
    // TODO:  public int ammoCapacity;
    // TODO:  public float reloadTime;
    // TODO:  public float rateOfFire; 

   protected void Update() {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot(fpsCam.transform.position, fpsCam.transform.forward);
            
        }
    }
   public void Shoot(Vector3 position, Vector3 direction ) {
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
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }

        
    }
}