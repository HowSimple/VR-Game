//https://www.youtube.com/watch?v=_QajrabyTJc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float Xsensitivity = 0.1f;
    public float Ysensitivity = 0.1f;

    float lookX, lookY;

    public GameObject player;
    public Camera fpsCam;
    float xRotation = 0f; 
   

     public void OnLook(InputAction.CallbackContext context)
    {
        
        lookX = context.ReadValue<Vector2>().x * Xsensitivity;
        lookY = context.ReadValue<Vector2>().y * Ysensitivity;
        Debug.Log("Look");
    }

  
   

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    // Update is called once per frame
    void Update()
    {   
        float x = lookX * Time.deltaTime;
        float y = lookY * Time.deltaTime;

        transform.Rotate(Vector3.up, x);

        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        Vector3 targetRotation = player.transform.eulerAngles;
        targetRotation.x = xRotation;
        fpsCam.transform.eulerAngles = targetRotation;


      ///  transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       //player.Rotate(Vector3.up * (x ));



      /*
       / float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float lookX = DeltaPointer.x 
      //  float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
        */

    }
}
