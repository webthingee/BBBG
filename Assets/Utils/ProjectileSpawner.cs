using System;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Spawn Details")]
    public MinMax spawnInterval;
    public List<GameObject> prefabProjectileList = new List<GameObject>();
    
    [Header("Multi Spawn Points")]
    public bool useMultiSpawnPoints;
    public MinMax multiSpawnPointsX;
    public MinMax multiSpawnPointsY;
    
    [Header("Transforms For Spawn Points")]
    public List<Transform> turretsList = new List<Transform>();
    
    private float spawnRate = 5f;
    private float nextSpawnTime = 1f;

    private void Awake()
    {
        spawnRate = Random.Range(spawnInterval.minValue, spawnInterval.maxValue);
    }

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
            var posX = (int) Random.Range(multiSpawnPointsX.minValue, multiSpawnPointsX.maxValue);
            var posY = (int) Random.Range(multiSpawnPointsY.minValue, multiSpawnPointsY.maxValue);
            
            spawnPoint = new Vector3(posX, posY, transform.position.z);
        }
        else if (turretsList.Any())
        {
            spawnPoint = turretsList[Random.Range(0, turretsList.Count)].position;
        }
        else
        {
            spawnPoint = transform.position;
        }
        
        int index = Random.Range(0, prefabProjectileList.Count);

        Instantiate(prefabProjectileList[index], spawnPoint, Quaternion.identity);
    }
}
