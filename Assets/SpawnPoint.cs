using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // objects to be spawned
    public List<GameObject> entities;

    public Transform spawnPoint;



    public void SpawnEntity(int index)
    {
        GameObject entity = entities[index];

        Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation);
       // yield return new WaitForSeconds(cooldownDuration);
    }
    public void SpawnEntityRandomly()
    { 
        
        SpawnEntity(Random.Range(0, entities.Count));
    }

    public void SpawnEntityFacingDirection(int index, Transform target)
    {
        GameObject entity = entities[index];
        GameObject spawnedEntity =  Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation);
        spawnedEntity.transform.LookAt(target);
    }
}
