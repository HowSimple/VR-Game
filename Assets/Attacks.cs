using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacks : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
       public void Fire(InputAction.CallbackContext context)
    {
        //Shoot(fpsCam.transform.position, fpsCam.transform.forward);
        
        Debug.Log("Fire!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
