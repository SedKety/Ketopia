using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IslandGenerator : MonoBehaviour
{
    public GameObject airship;
    public Vector3 lastSpawnedIslands;

    public Collider islandsSpawnPoint;
    public chunk currentChunk;

    public int spawnDistance;

    public GameObject[] islands;
    public chunk[] chunks;
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
        for (int i = 0; i < currentChunk.howManyToSpawn; i++)
        {
            var randomSpawnLocation = SpawnPosition(islandsSpawnPoint.bounds);
            var randomRarity = Random.Range(0, 100);
            GameObject islandTospawn = null;
            if (randomRarity < 75)
            {
                islandTospawn = currentChunk.commonIslands[Random.Range(0, currentChunk.commonIslands.Length)];
            }
            else if (randomRarity < 90 & randomRarity >= 75)
                {
                islandTospawn = currentChunk.rareIslands[Random.Range(0, currentChunk.rareIslands.Length)];
            }
            else if ((randomRarity >= 90))
            {
                islandTospawn = currentChunk.legendaryIslands[Random.Range(0, currentChunk.legendaryIslands.Length)];
            }
            if(islandTospawn != null)
            {
                var spawnedIsland = Instantiate(islandTospawn, randomSpawnLocation, Quaternion.identity);
            }
        }
        lastSpawnedIslands = airship.transform.position;
        if(currentChunk.name == "SpawnChunk")
        {
            var randomChunk = Random.Range(0, chunks.Length);
            currentChunk = chunks[randomChunk];
        }
        var getAnotherChunk = Random.Range(0, 6);
        if(getAnotherChunk == 0 )
        {
            var randomChunk = Random.Range(0, chunks.Length);
            currentChunk = chunks[randomChunk];
        }
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
    public struct chunk
    {
        [Header("ChunkName")]
        public string name;
        public int howManyToSpawn;
        public Color chunkColour;
        public GameObject[] commonIslands;
        public GameObject[] rareIslands;
        public GameObject[] legendaryIslands;
        public Color chunkColour1;
    }
}





