using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun activeWeapon;
    
    public Gun secondWeapon;
    public Gun[] weapons;
    public int activeWeaponIndex;
    private Character charController;

    void Start() {
        charController = this.GetComponent<Character>();

        if (secondWeapon != null)
        secondWeapon.gameObject.SetActive(false);
        activeWeaponIndex = 0;

   


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
         
        if(activeWeapon.gameObject.activeSelf)
       {
           activeWeapon.gameObject.SetActive(false);
           secondWeapon.gameObject.SetActive(true);
       }
      else if (secondWeapon.gameObject.activeSelf)
       {
           activeWeapon.gameObject.SetActive(true);
           secondWeapon.gameObject.SetActive(false);
       }
    }
    public void ShootGun()
    {
        
        
        StartCoroutine(activeWeapon.Shoot(charController.damageModifier));
    }
        public void ShootGun(float damageModifier)
    {
        
        
        StartCoroutine(activeWeapon.Shoot(charController.damageModifier));
    }




  
}
