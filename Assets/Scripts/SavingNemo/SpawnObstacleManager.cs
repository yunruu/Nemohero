using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector2[] lanes = new Vector2[3];
    private float spawnPosx = 9.3f;

    private float obstacleStart = 0.5f;
    private float obstacleInterval = 0.8f;
    //private float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        lanes[0] = new Vector2(spawnPosx, -3.4f);
        lanes[1] = new Vector2(spawnPosx, 0);
        lanes[2] = new Vector2(spawnPosx, 3.4f);
        InvokeRepeating("SpawnRandomObstacle", obstacleStart, obstacleInterval);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnRandomObstacle()
    {
        int laneIndex = Random.Range(0, lanes.Length);
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], lanes[laneIndex],
            obstaclePrefabs[obstacleIndex].transform.rotation);
    }

   /** public void ChangeSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }*/

}
