using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    IShooter weapon;
    
    
   
    public void ShootGun() => weapon.Shoot();
}
