using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
     public float damage = 10f;
    public float range = 100f;
    public Transform muzzle;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    void Update() {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }
    void Shoot() {
        RaycastHit hit;
        muzzleFlash.Play();

       
           // float radius, x_rotation, y_rotation;

            Vector3 forward = fpsCam.transform.position;

            Vector3 targetPosition  = Quaternion.Euler(0, 45 , 0) * forward;

        
        if (Physics.Raycast(fpsCam.transform.position, targetPosition, out hit, range))
        {
            Debug.Log(hit.transform.name);
           Health h =  hit.transform.GetComponent<Health>();
            if (h != null)
            {
                h.takeDamage(damage);
            }

        }
    }
}
