using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public bool isDashing = false;
    public float dashDistance = 10f;
    public float dashDuration = 2.50f;


    //public Camera playerCam;
    public Transform origin;
    public GameObject player;


    // TODO: move to player input controller class
    public void DashForward(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
        
        
           //Vector3 destination = player.transform.forward * dashDistance;
            Vector3 destination = origin.transform.forward * dashDistance;
           StartCoroutine(DashOverTime(player, destination, dashDuration));


        


    }
  

    public IEnumerator DashOverTime(GameObject movingObject, Vector3 destination, float duration)
    {
        float timeElapsed = 0;
        Vector3 start = movingObject.transform.position;

        while (timeElapsed <duration)
        {
            movingObject.transform.position = Vector3.Lerp(start, destination, (timeElapsed/duration));
            //player.GetComponent<CharacterController>().Move(Vector3.Lerp(start, destination, (timeElapsed/duration)));
            timeElapsed += Time.deltaTime;
             yield return new WaitForEndOfFrame();
        }
         //player.GetComponent<CharacterController>().Move(destination);
       movingObject.transform.position = destination;
        //player.GetComponent<CharacterController>().Move(Vector3.MoveTowards(player.transform.forward ,direction, dashDistance)

    }
}
