using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun activeWeapon;
    public GameObject weaponObject;
    void Start(){
               
        //weaponObject = GameObject.Find("Enemy Rifle");
        
        //weaponObject = transform.Find("Enemy Rifle").gameObject;

        activeWeapon =  weaponObject.GetComponent<Gun>();

        StartCoroutine(ShootGun());
        


    }

   
    public IEnumerator ShootGun() => activeWeapon.Shoot();

    void Update(){
        StartCoroutine(ShootGun());

    }
}
