using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public bool isDashing = false;
    public float dashDistance = 5f;
    public float dashDuration = 2.50f;

    public Camera playerCam;
    public GameObject player;

    void Update()
    {
        
        if(!isDashing && Input.GetButtonDown("Sprint") )
        {
           
           Vector3 destination = player.transform.forward * dashDistance;
        
           StartCoroutine(DashOverTime(player, destination, dashDuration));


        }   

        
    }
  

    public IEnumerator DashOverTime(GameObject movingObject, Vector3 destination, float duration)
    {
        float timeElapsed = 0;
        Vector3 start = movingObject.transform.position;

        while (timeElapsed <duration)
        {
            //movingObject.transform.position = Vector3.Lerp(start, destination, (timeElapsed/duration));
            player.GetComponent<CharacterController>().Move(Vector3.Lerp(start, destination, (timeElapsed/duration)));
            timeElapsed += Time.deltaTime;
             yield return new WaitForEndOfFrame();
        }
         player.GetComponent<CharacterController>().Move(destination);
       //movingObject.transform.position = destination;
        //player.GetComponent<CharacterController>().Move(Vector3.MoveTowards(player.transform.forward ,direction, dashDistance)

    }
}
