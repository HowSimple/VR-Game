using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;


    public float timeBetweenWaves;
    private int remainingEnemies;
    public int wavesRemaining;
    private int currentWave;
   
    public Transform player;
    public List<GameObject> aliveEnemies;
    public TMP_Text waveUI;
    // Start is called before the first frame update
    void Start()
    {
       currentWave = 0;
        
        StartCoroutine(startWaves());
    }
    private void FixedUpdate()
    {
        waveUI.text = "Wave "  +currentWave.ToString()+" of "+wavesRemaining.ToString();
    }
    public IEnumerator startWaves()
    {
        Debug.Log("Wave started");
        for (int i = 0; i < wavesRemaining; i++)
        {
            NewWave();
            Debug.Log("Wave started");
            currentWave++;
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

}
