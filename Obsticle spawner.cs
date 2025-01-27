using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticlespawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    public float spawnInterval = 2f; 
    public float spawnRangeX = 5f; 
    public float spawnDistance = 30f; 

    private Transform playerTransform; 

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnInterval);
            
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            
            Vector3 spawnPosition = new Vector3(randomX, 0f, playerTransform.position.z + spawnDistance);
            
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacleToSpawn = obstaclePrefabs[randomIndex];
            
            Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
