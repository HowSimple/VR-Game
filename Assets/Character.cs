using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    GunController gunController;


     void Update(){
        StartCoroutine(gunController.ShootGun());

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
