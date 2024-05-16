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
    public void Start()
    { 
        currentChunk = chunks[0];
    }
    public void Update()
    {
        distance = (Vector3.Distance(lastSpawnedIslands, airship.transform.position));
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
            else if (randomRarity < 99 & randomRarity >= 75)
                {
                islandTospawn = currentChunk.rareIslands[Random.Range(0, currentChunk.rareIslands.Length)];
            }
            else if ((randomRarity >= 99))
            {
                
                islandTospawn = currentChunk.legendaryIslands[Random.Range(0, currentChunk.legendaryIslands.Length)];
                print(islandTospawn.name);
            }
            if(islandTospawn != null)
            {
                var spawnedIsland = Instantiate(islandTospawn, randomSpawnLocation, Quaternion.identity);
                spawnedIsland.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
                islands.Add(spawnedIsland);
                if (islands.Count >= maxIslands)
                {
                    for (int j = 0; j < islandsToRemove; j++)
                    {
                        Destroy(islands[j]);
                        islands.Remove(islands[j]);
                    }
                }
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





