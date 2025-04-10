using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnvironment : MonoBehaviour
{
    public List<GameObject> objectToSpawnPrefab;

    [HideInInspector] public Vector2 currentGravity;
    public float minTimeDelay;
    public float maxTimeDelay;

    public float nextSpawnTime = 0.5f; //when the next object should spawm

    public Transform[] spawnPoints;
    private int randomSpawnSpot;

    public int TotalCountOfSpawnedObjects;

    private void Start()
    {
        currentGravity = Physics2D.gravity;
        randomSpawnSpot = Random.Range(0, spawnPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawnTime)
        {
            SpawnObjects();
            nextSpawnTime = Time.time + Random.Range(minTimeDelay, maxTimeDelay);
        }
    }

    private void SpawnObjects()
    {
        TotalCountOfSpawnedObjects = 0;
        
        randomSpawnSpot = Random.Range(0, spawnPoints.Length);

        while (TotalCountOfSpawnedObjects == 0)
        {
            int randomPrefabIndex = Random.Range(0, objectToSpawnPrefab.Count);
            GameObject prefabToSpawn = objectToSpawnPrefab[randomPrefabIndex];

            prefabToSpawn = Instantiate(prefabToSpawn, spawnPoints[randomSpawnSpot].transform.position, Quaternion.identity);
            prefabToSpawn.GetComponent<Rigidbody2D>().linearVelocityY = TrollyController.instance.trollySpeed;
            TotalCountOfSpawnedObjects++;
        }
    }
}
