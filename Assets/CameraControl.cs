using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform playerBody;
    public Camera fpsCam;
    float xRotation = 0f;
    
    private float lookX;
    private float lookY;
    
   
    public void OnLook(InputAction.CallbackContext context)
    {
        
        lookX = context.ReadValue<Vector2>().x;
        lookY = context.ReadValue<Vector2>().y;
        Debug.Log("Look");
    }

    // Update is called once per frame
    void Update()
    {
        xRotation -= lookY * sensitivity;
         xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * (lookX * sensitivity));
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
