using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorySpawnManager : MonoBehaviour
{
    public GameObject coralPrefab;
    private float start = 0.1f;
    private float interval = 2f;
    private float min = -1f;
    private float max = 1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCorals", start, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCorals()
    {
        GameObject corals = Instantiate(coralPrefab, transform.position, Quaternion.identity);
        corals.transform.position += Vector3.up * Random.Range(min, max);
    }
}
