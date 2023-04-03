using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // get the enemy spawner
    EnemySpawner enemySpawner;

    // updaate the wave config so it can be used in the enemy spawner 
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // get the wave config from the enemy spawner
    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();

        // get the waypoints from the wave config
        waypoints = waveConfig.GetWaypoints();
        // update the position of the enemy to the first waypoint
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

// moving the enemy to the next waypoint
    void FollowPath()
    {
        if(waypointIndex < waypoints.Count)
        {
            // get the position of the next waypoint
            Vector3 targetPosition = waypoints[waypointIndex].position;
            // move the enemy towards the next waypoint
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            // update the waypoint index when the enemy reaches the next waypoint
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            // destroy the enemy when it reaches the end of the path
            Destroy(gameObject);
        }
    }
}
