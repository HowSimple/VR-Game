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
 
   

    public void ShootGun()
    {
        
        
        StartCoroutine(activeWeapon.Shoot());
    }
        public void ShootGun(float damageModifier)
    {
        
        
        StartCoroutine(activeWeapon.Shoot(charController.damageModifier));
    }




  
}
