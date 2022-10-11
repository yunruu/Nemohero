using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSpawner : MonoBehaviour
{
    public GameObject prefab;
    protected float minSpawnRate = 5;
    protected float maxSpawnRate = 1;
    protected float fixedMin;
    protected float fixedMax;
    public float startTime = 2.5f;
    private float interval;

    private static float minHeight = -1.5f;
    private static float maxHeight = 1.5f;

    private void Awake()
    {
        this.fixedMin = this.minSpawnRate;
        this.fixedMax = this.maxSpawnRate;
    }

    private void OnEnable()
    {
        Invoke(nameof(Spawn), 1f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject spawned = Instantiate(prefab, transform.position, Quaternion.identity);
        spawned.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

        interval = Random.Range(minSpawnRate, maxSpawnRate);
        
        Invoke(nameof(Spawn), interval);
    }

    public void IncreaseMaxRate()
    {
        maxSpawnRate -= 0.25f;
    }

    public void IncreaseMinRate()
    {
        minSpawnRate -= 0.25f;
    }

    public bool MinMoreThan(float val)
    {
        return this.minSpawnRate > val;
    }

    public bool MaxMoreThan(float val)
    {
        return this.maxSpawnRate > val;
    }

    public void Reset()
    {
        minSpawnRate = fixedMin;
        maxSpawnRate = fixedMax;
    }
}
