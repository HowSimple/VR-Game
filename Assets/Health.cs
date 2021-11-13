using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float healthPoints = 50f;
    public bool healthRegenEnabled = false;
    public float regenRate;
    public float regenDelay;


    void enableRegen()
    {
        healthRegenEnabled = true;
    }
    public void FixedUpdate()
    {
        if (healthRegenEnabled)
            healthPoints += regenRate * Time.deltaTime;

    }
    public void PlayerDeath()
    {

    }
    public void takeDamage(float amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0f)
        {
           if(gameObject.name == "Player")
                PlayerDeath();
           else Die();

        }



    }
    
    void Die()
    {
        Destroy(gameObject);

    }

   
}
