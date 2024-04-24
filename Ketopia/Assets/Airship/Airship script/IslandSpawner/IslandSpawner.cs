using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IslandSpawner : MonoBehaviour
{
    public GameObject[] islands;
    [SerializeField] int howManyToSpawn;
    [SerializeField] Collider whereToSpawn;
    public static IslandSpawner _instance;
    public void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    [ContextMenu("SpawnIslands")]
    public void SpawnIslands()
    {
        for (int i = 0; i < howManyToSpawn; i++)
        {
            var randomSpawnLocation = SpawnPosition(whereToSpawn.bounds);
           var spawnedIsland =  Instantiate(islands[Random.Range(0, islands.Length)], randomSpawnLocation, Quaternion.identity);
        }
    }

    Vector3 SpawnPosition(Bounds bounds)
    {
        float randomY = Random.Range(bounds.max.y, bounds.min.y);
        float randomX = Random.Range(bounds.max.x, bounds.min.x);
        float randomZ = Random.Range(bounds.max.z, bounds.min.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
