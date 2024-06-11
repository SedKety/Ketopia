using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Chunk
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
public class ChunkScript : MonoBehaviour
{
    public Chunk chunk;
    public int chunkCount;
    public Collider islandsSpawnPoint;

    public List<GameObject> islands;

    private int islandsSpawnedThisCycle = 0;

    public bool hasBeenTriggered;

    public int maxRetries;
    private void Start()
    {
        hasBeenTriggered = false;
        islandsSpawnPoint = GetComponent<Collider>();
        if(chunkCount > 0)
        {
            StartSpawningIslands();
        }
    }

    private void StartSpawningIslands()
    {
        islandsSpawnedThisCycle = 0;
        StartCoroutine(SpawnIslands());
    }

    private IEnumerator SpawnIslands()
    {
        while (islandsSpawnedThisCycle < chunk.howManyToSpawn)
        {
            var randomRarity = Random.Range(0, 100);
            GameObject islandToSpawn = null;
            if (randomRarity < 75)
            {
                islandToSpawn = chunk.commonIslands[Random.Range(0, chunk.commonIslands.Length)];
            }
            else if (randomRarity < 99 && randomRarity >= 75)
            {
                islandToSpawn = chunk.rareIslands[Random.Range(0, chunk.rareIslands.Length)];
            }
            else if (randomRarity >= 99)
            {
                islandToSpawn = chunk.legendaryIslands[Random.Range(0, chunk.legendaryIslands.Length)];
            }

            if (islandToSpawn != null)
            {
                var randomSpawnLocation = SpawnPosition(islandsSpawnPoint.bounds);
                randomSpawnLocation.y += 100;
                for(int i = 0; i < maxRetries; i++)
                {
                    if (!Physics.Raycast(randomSpawnLocation, Vector3.down))
                    {
                        var spawnedIsland = Instantiate(islandToSpawn, randomSpawnLocation, Quaternion.identity, gameObject.transform);
                        spawnedIsland.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
                        islands.Add(spawnedIsland);
                        break;
                    }
                }
            }

            islandsSpawnedThisCycle++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    Vector3 SpawnPosition(Bounds bounds)
    {
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(hasBeenTriggered == false)
            {
                hasBeenTriggered = true;
                ChunkSpawner.SpawnChunk(transform);
            }
            else
            {
                foreach (var island in islands)
                {
                    island.SetActive(true);
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TurnOnChunk"))
        {
            foreach (var island in islands)
            {
                island.SetActive(false);
            }
        }
    }
}
