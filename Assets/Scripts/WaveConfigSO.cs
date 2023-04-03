using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")] 
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List <GameObject> enemyPrefabs;
   
    // Start is called before the first frame update
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;

// get the time between spawns
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

// get the number of enemies in the wave
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

// get the enemy prefab at the specified index
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }


    // Update is called once per frame
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

// get the waypoints from the path
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

// spawn the enemies in the wave and care about it's position
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, 
                                    timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
