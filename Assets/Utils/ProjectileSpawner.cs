using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct MinMax
{
    public float minValue;
    public float maxValue;
}

public class ProjectileSpawner : MonoBehaviour
{
    public List<GameObject> fieldObjectsList = new List<GameObject>();
    
    public MinMax spawnInterval;
    public bool useMultiSpawnPoints;
    
    private float spawnRate = 5f;
    [SerializeField] private float nextSpawnTime = 1f;

    private void Update()
    {
        if (!(Time.time > nextSpawnTime)) return;
        
        SpawnPrefab();
        spawnRate = Random.Range(spawnInterval.minValue, spawnInterval.maxValue);
        nextSpawnTime = Time.time + spawnRate;
    }

    private void SpawnPrefab()
    {
        Vector3 spawnPoint;
        
        if (useMultiSpawnPoints)
        {
            spawnPoint = new Vector3(16f, (int)Random.Range(-5f, 4f), transform.position.z);
        }
        else
        {
            spawnPoint = transform.position;
        }
        
        int index = Random.Range(0, fieldObjectsList.Count);

        GameObject fieldObj = Instantiate(fieldObjectsList[index], spawnPoint, Quaternion.identity);
    }
}
