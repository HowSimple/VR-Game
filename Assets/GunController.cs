using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Gun activeWeapon;
    
    
   
    public void ShootGun() => activeWeapon.Shoot();


}
