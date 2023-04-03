using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool isLooping;

    // get the wave configs so we can spawn waves of enemies
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;

    void Start()
    {
       StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    // spawn the enemies in the wave and care about it's position
    IEnumerator SpawnEnemyWaves()
    {   
        // first loop that spawns the waves
        do 
        {
            
        // second loop that spawns the enemies in the wave
        foreach(WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
        // looping through the enemies and spawning them in the wave
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(i), 
                        currentWave.GetStartingWaypoint().position, 
                        Quaternion.Euler(0,0,180),
                        transform);
            
            // updating so the enemies wait for the next enemy to spawn
            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
        yield return new WaitForSeconds(timeBetweenWaves);
            }
            // end the loop if the wave is not looping
        } while (isLooping);
    }
}


