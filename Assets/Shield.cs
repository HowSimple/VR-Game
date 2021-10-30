using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    //particle effet
    float radius;
    float length;
    GameObject shieldPrefab;
    
    bool isActive;

    void start()
    { 
        
    }
    void activateShield(int duration)
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, transform.rotation);

    }
}
