using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public float healthPoints = 50f;
    public float maxHP;
    public bool healthRegenEnabled = false;
    public float regenRate;
    public float regenDelay;
    public GameObject healingItemDrop;
    void Start(){
        maxHP = healthPoints;
    }
  
    public void FixedUpdate()
    {
        if (healthRegenEnabled)
            healthPoints += regenRate * Time.deltaTime;

    }
    public void heal(float amount)
    {
        healthPoints += amount;
        if (healthPoints > maxHP)
        {
            healthPoints = maxHP;
        }
    }
    public void takeDamage(float amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0f)
        {
           Die();
        }
    }
    
    IEnumerator PlayerDeath()
    {
        //QuitGame();
        Transform overlay = gameObject.transform.Find("Player Death Overlay");

        overlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       

    }

    void Die()
    {
       if (gameObject.name == "Player")
        {
             StartCoroutine(PlayerDeath());
        }
       else 
       {
            // add wait to delete
            Instantiate(healingItemDrop, gameObject.transform);
            Destroy(gameObject);

       }
        

    }

   
}
