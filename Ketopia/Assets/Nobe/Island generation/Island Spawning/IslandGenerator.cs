using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public GameObject airship;
    public Vector3 lastSpawnedIslands;

    public Collider islandsSpawnPoint;

    public int spawnDistance;

    public GameObject[] islands;
    public int howManyToSpawn;

    public island[] commonIslands;
    public island[] uncommonIslands;
    public island[] rareIslands;
    public island[] epicIslands;
    public island[] legendaryIslands;
    [ContextMenu("SpawnIslands")]

    public void Start()
    {
        Invoke(nameof(SpawnIslands), 0.1f);
    }
    public void StartSpawning()
    {
        airship = AirshipManager.instance.gameObject;
        islandsSpawnPoint = GameObject.FindGameObjectWithTag("IslandSpawnBox").GetComponent<Collider>();
    }
    public void Update()
    {
        if (Vector3.Distance(lastSpawnedIslands, airship.transform.position) >= spawnDistance)
        {
            SpawnIslands();
        }
    }

    public void SpawnIslands()
    {
        for (int i = 0; i < howManyToSpawn; i++)
        {
            var randomSpawnLocation = SpawnPosition(islandsSpawnPoint.bounds);
            var spawnedIsland = Instantiate(islands[Random.Range(0, islands.Length)], randomSpawnLocation, Quaternion.identity);
        }
        lastSpawnedIslands = airship.transform.position;
    }

    Vector3 SpawnPosition(Bounds bounds)
    {
        float randomY = Random.Range(bounds.max.y, bounds.min.y);
        float randomX = Random.Range(bounds.max.x, bounds.min.x);
        float randomZ = Random.Range(bounds.max.z, bounds.min.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Island"))
        {
            Destroy(other.gameObject);
        }
    }

    [System.Serializable]
    public struct island
    {
        [Header("IslandName")]
        string name;
        [Space]
        [Space]
        [Header("IslandObject")]
        [SerializeField] GameObject islandGameobject;
        [Space]
        [Space]
        [Header("ResourceNodes")]
        [SerializeField] GameObject[] nodesToSpawnOnIsland;
        [SerializeField] int howManyNodes;
    }
}





