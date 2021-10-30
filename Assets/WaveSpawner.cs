using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;


    public float timeBetweenWaves;
    private int remainingEnemies;
    public int wavesRemaining;
   
   
    public Transform player;
    public List<GameObject> aliveEnemies;
    // Start is called before the first frame update
    void Start()
    {
       
        
        StartCoroutine(startWaves());
    }
    public IEnumerator startWaves()
    {
        Debug.Log("Wave started");
        for (int i = 0; i < wavesRemaining; i++)
        {
            NewWave();
            Debug.Log("Wave started");
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("Waves ended");

    }

    void NewWave()
    {
        foreach (SpawnPoint point in spawnPoints)
        {
           aliveEnemies.Add(point.SpawnEntityFacingDirection(0, player));

        }


    }
    // Update is called once per frame
    void Update()
    {
      
            

    }
}
