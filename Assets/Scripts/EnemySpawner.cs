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

    [Header("Rotation")]
    [SerializeField] private bool applyCustomRotation = false;

    private List<GameObject> spawnedBosses = new List<GameObject>();


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
    do
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            if (currentWave == null)
            {
                Debug.LogError("Current wave is null.");
                continue;
            }

            if (currentWave.IsBossWave())
            {
                SpawnBosses();
                yield return new WaitUntil(() => !IsBossActive());
            }
            else
            {
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Quaternion rotation = applyCustomRotation ? Quaternion.Euler(0, 0, 180) : Quaternion.identity;
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartingWaypoint().position,
                                rotation,
                                transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    } while (isLooping);
}



  private void SpawnBosses()
{
    Debug.Log("Spawning Bosses");
    // spawning bosses
    for (int i = 0; i < currentWave.GetBossPrefabs().Count; i++)
    {
        Quaternion rotation = applyCustomRotation ? Quaternion.Euler(0, 0, 180) : Quaternion.identity;
        GameObject boss = Instantiate(currentWave.GetBossPrefabs()[i], 
                        currentWave.GetStartingWaypoint().position, 
                        rotation,
                        transform);
        spawnedBosses.Add(boss);
    }
}

public bool IsCurrentWaveBoss()
{
    return currentWave != null && currentWave.IsBossWave();
}

private bool IsBossActive()
{
    return spawnedBosses.Count > 0 && spawnedBosses.Exists(boss => boss != null);
}



private bool AreAllBossesDestroyed()
{
    for (int i = spawnedBosses.Count - 1; i >= 0; i--)
    {
        if (spawnedBosses[i] == null)
        {
            spawnedBosses.RemoveAt(i);
        }
    }
    return spawnedBosses.Count == 0;
}


}
