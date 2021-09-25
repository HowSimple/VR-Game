using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public int projectilesPerShot = 15;
    public float spreadAngle = 10f;
    public float maxSpread = 0.04f;

       protected new void Update() {
        if (Input.GetButtonDown("Fire"))
        {
            //Shoot(fpsCam.transform.position, fpsCam.transform.forward);
            ShotgunSpread();
        }
    }

   void ShotgunSpread(){
        Vector3 gunDirection = fpsCam.transform.forward;
        for (int i = 0; i < projectilesPerShot; i++)
        {
       
            Vector3 spreadDirection = fpsCam.transform.forward;
            //spreadDirection.x += Random.Range(-maxSpread, maxSpread);
            spreadDirection.y += Random.Range(-maxSpread, maxSpread);
           // spreadDirection.z += Random.Range(-maxSpread, maxSpread);

        
            Color color = new Color(55,55,55);
            Debug.DrawLine(spreadDirection, new Vector3(5, 0, 0), color);

            Shoot(gunDirection, spreadDirection);      




        }

    }
}