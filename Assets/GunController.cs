using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun activeWeapon;
    public GameObject weaponObject;
    void Start() {


        activeWeapon = weaponObject.GetComponent<Gun>();

       


    }


    public void ShootGun()
    {
        StartCoroutine(activeWeapon.Shoot());
    }

    void Update(){
        //if(activeWeapon.allowFire)
            ShootGun();

    }
}
