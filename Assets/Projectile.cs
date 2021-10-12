using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float maxSpread = 0;
    public float projectilesPerShot = 1;
    public Rigidbody body;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 10 - GetComponent<Rigidbody>().velocity);
    }
}
