using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float healthPoints = 50f;

    public void takeDamage(float amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0f)
        {
            Die();
        }

    }
  
    void Die()
    {
        Destroy(gameObject);

    }

   
}
