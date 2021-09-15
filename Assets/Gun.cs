using UnityEngine;

public class Gun : MonoBehavior {

    public float damage = 10f;
    public float range = 100f;


    void Update() {
        if (Input.GetButtonDown("First"))
        {
            Shoot();
        }
    }
    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}