using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    public GameObject proj;
    public float launchVelocity = 700f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fire(InputAction.CallbackContext context)
    {
        Shoot();
    }

      void Shoot()
    {
        GameObject rocket = Instantiate(proj, transform.position, transform.rotation);
        rocket.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity, 0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
