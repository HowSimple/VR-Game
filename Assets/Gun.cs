using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;

    void Update() {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }
    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
           Health h =  hit.transform.GetComponent<Health>();
            if (h != null)
            {
                h.takeDamage(damage);
            }

        }
    }
}