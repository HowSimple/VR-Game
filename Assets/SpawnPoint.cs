using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // objects to be spawned
    public List<GameObject> entities;

    public Transform spawnPoint;



    public GameObject SpawnEntity(int index)
    {
        GameObject entity = Instantiate(entities[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
        // yield return new WaitForSeconds(cooldownDuration);
        return entity;
    }
    public GameObject SpawnEntityRandomly(Transform direction)
    { 
       
        return SpawnEntityFacingDirection(Random.Range(0, entities.Count), direction);
    }

    public GameObject SpawnEntityFacingDirection(int index, Transform target)
    {
        GameObject entity = entities[index];
        GameObject spawnedEntity =  Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation);
        spawnedEntity.transform.LookAt(target);
        return spawnedEntity;
    }
}
