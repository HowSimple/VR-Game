using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public bool isDashing = false;
    public float dashDistance = 50f;
    public float dashDuration = 2.50f;

    public Camera playerCam;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    } 

    // Update is called once per frame
    void Update()
    {
        
        if(!isDashing && Input.GetButtonDown("Sprint") )
        {
           // DashTowards(player.transform.forward);
           Vector3 destination = player.transform.forward * dashDistance;
           StartCoroutine(DashOverTime(player, destination, dashDuration));

           //startDashCoroutine(playerCam.transform.forward);
        }   

        
    }
    void DashTowards(Vector3 direction)
    {
      
       // /player.GetComponent<CharacterController>().Move(Vector3.MoveTowards(player.transform.forward ,direction, dashDistance) );

    }

    public IEnumerator DashOverTime(GameObject movingObject, Vector3 destination, float duration)
    {
        float timeElapsed = 0;
        Vector3 start = movingObject.transform.position;

        while (timeElapsed <duration)
        {
            movingObject.transform.position = Vector3.Lerp(start, destination, (timeElapsed/duration));
            timeElapsed += Time.deltaTime;
             yield return new WaitForEndOfFrame();
        }

       movingObject.transform.position = destination;
        //player.GetComponent<CharacterController>().Move(Vector3.MoveTowards(player.transform.forward ,direction, dashDistance)

    }
}
