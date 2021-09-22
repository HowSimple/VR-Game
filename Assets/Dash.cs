using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public bool isDashing = false;
    public float dashDistance = 50f;
    public float dashDuration = 0.50f;

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
            DashTowards(player.transform.forward);


           //startDashCoroutine(playerCam.transform.forward);
        }   

        
    }
    void DashTowards(Vector3 direction)
    {
      
        player.GetComponent<CharacterController>().Move(Vector3.MoveTowards(player.transform.position ,direction, dashDistance) );

    }

    public IEnumerator DashOverTime(GameObject target, Vector3 destination, float duration)
    {
        float timeElapsed = 0;
        Vector3 start = target.transform.position;

        while (timeElapsed <duration)
        {
            target.transform.position = Vector3.Lerp(start, destination, (timeElapsed/duration));
            timeElapsed += Time.deltaTime;

        }
        /*isDashing = true;
        float speed = player.GetComponent<PlayerMovement>().walkSpeed;

       // float speed = player.GetComponent<PlayerMovement>().walkSpeed;
       // player.PlayerMovement.walkSpeed *= 2;
        speed *= 2;

        */
        yield return new WaitForSeconds(dashDuration);
        




       // speed /= 2;
        isDashing = false;


    }
}
