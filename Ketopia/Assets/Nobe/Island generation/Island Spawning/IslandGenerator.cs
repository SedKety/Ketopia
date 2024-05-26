using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IslandGenerator : MonoBehaviour
{
    public GameObject airship;
    public Vector3 lastSpawnedIslands;

    public Collider islandsSpawnPoint;
    public chunk currentChunk;

    public chunk[] chunks;

    public float distance;
    public int spawnDistance;

    public List<GameObject> islands;
    public int maxIslands;
    public int islandsToRemove;

    private int islandsSpawnedThisCycle = 0;
    private bool spawningIslands = false;

    private void Start()
    {
        currentChunk = chunks[0];
        StartSpawningIslands();
    }

    private void Update()
    {
        distance = Vector3.Distance(lastSpawnedIslands, airship.transform.position);
        if (distance >= spawnDistance && !spawningIslands)
        {
            StartSpawningIslands();
        }

        if (spawningIslands)
        {
            SpawnIslands();
        }
    }

    private void StartSpawningIslands()
    {
        islandsSpawnedThisCycle = 0;
        spawningIslands = true;
    }

    private void SpawnIslands()
    {
        if (islandsSpawnedThisCycle < currentChunk.howManyToSpawn)
        {
            var randomRarity = Random.Range(0, 100);
            GameObject islandToSpawn = null;
            if (randomRarity < 75)
            {
                islandToSpawn = currentChunk.commonIslands[Random.Range(0, currentChunk.commonIslands.Length)];
            }
            else if (randomRarity < 99 && randomRarity >= 75)
            {
                islandToSpawn = currentChunk.rareIslands[Random.Range(0, currentChunk.rareIslands.Length)];
            }
            else if (randomRarity >= 99)
            {
                islandToSpawn = currentChunk.legendaryIslands[Random.Range(0, currentChunk.legendaryIslands.Length)];
                print(islandToSpawn.name);
            }

            if (islandToSpawn != null)
            {
                var randomSpawnLocation = SpawnPosition(islandsSpawnPoint.bounds);
                var spawnedIsland = Instantiate(islandToSpawn, randomSpawnLocation, Quaternion.identity);
                spawnedIsland.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
                islands.Add(spawnedIsland);

                if (islands.Count >= maxIslands)
                {
                    for (int j = 0; j < islandsToRemove; j++)
                    {
                        Destroy(islands[j]);
                        islands.RemoveAt(j);
                    }
                }
            }

            islandsSpawnedThisCycle++;
        }
        else
        {
            lastSpawnedIslands = airship.transform.position;
            spawningIslands = false;

            if (currentChunk.name == "SpawnChunk")
            {
                var randomChunk = Random.Range(0, chunks.Length);
                currentChunk = chunks[randomChunk];
            }

            var getAnotherChunk = Random.Range(0, 6);
            if (getAnotherChunk == 0)
            {
                var randomChunk = Random.Range(0, chunks.Length);
                currentChunk = chunks[randomChunk];
            }
        }
    }

    private Vector3 SpawnPosition(Bounds bounds)
    {
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}





