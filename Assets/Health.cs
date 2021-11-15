using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
       // overlay.GetComponent<RectTransform>()
       // Instantiate(deathScreen);

    }
    void Die()
    {
       if (gameObject.name == "Player")
        {
             StartCoroutine(PlayerDeath());
        }
       else 
         Destroy(gameObject);

    }

   
}
