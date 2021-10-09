using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System;
public class PlayerInputController : MonoBehaviour
{
    public GunController gunController;
    

    
    public void Fire(InputAction.CallbackContext context)
    {
        
        //GunController.ShootGun();
        Vector3 start = transform.position;
    
        Debug.Log("Fire!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
