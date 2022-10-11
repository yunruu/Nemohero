using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerupManager : MonoBehaviour
{
    public GameObject[] powerupPrefabs;
    private Vector2[] lanes = new Vector2[3];
    private float spawnPosx = 9.3f;

    private float powerupStart = 2;
    private float powerupInterval = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        lanes[0] = new Vector2(spawnPosx, -3.4f);
        lanes[1] = new Vector2(spawnPosx, 0);
        lanes[2] = new Vector2(spawnPosx, 3.4f);

        InvokeRepeating("SpawnRandomPowerup", powerupStart, powerupInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomPowerup()
    {
        int laneIndex = Random.Range(0, lanes.Length);
        int powerupIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[powerupIndex], lanes[laneIndex],
            powerupPrefabs[powerupIndex].transform.rotation);
    }
}
