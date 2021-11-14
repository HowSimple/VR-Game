using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun activeWeapon;
    
    public Gun secondWeapon;
    public Gun[] weapons;
    public int activeWeaponIndex;
    //public GameObject weaponObject;
    

    void Start() {
        if (secondWeapon != null)
        secondWeapon.gameObject.SetActive(false);
        activeWeaponIndex = 0;

        //activeWeapon = GetComponent<Gun>();
    //if (weapons.Length == 0 && activeWeapon != null)
        //[0] = activeWeapon;
       // activeWeapon = weapons[0];
       


    }
    public void ReloadWeapon()
    {
        activeWeapon.Reload();
    }
    public void SwitchWeapon2()
    {
        //https://www.youtube.com/watch?v=Dn_BUIVdAPg
     
        
        int i = 0;
        foreach(Transform weapon in transform)
        {

            // check if this child object has a Gun component, so children that aren't weapons aren't effected
            if(gameObject.GetComponent<Gun>() != null)
            {
                if (i == activeWeaponIndex)
                weapon.gameObject.SetActive(true);
                else 
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

    }
    public void SwitchWeapon()
    {
         
           Gun temp = activeWeapon;
        activeWeapon = secondWeapon;
        secondWeapon = temp;
      
    }
    public void ShootGun()
    {
        StartCoroutine(activeWeapon.Shoot());
    }
    public void ShootTarget(Vector3 direction)
    {

    }

  
}
