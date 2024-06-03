using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunk;
    public static GameObject chunkToSpawn;
    public static List<GameObject> chunks = new List<GameObject>();
    public static int chunkCounter;

    public static chunk currentChunk;
    public List<chunk> placeHolder;
    public static List<chunk> chunkTypes;

    public static int maxChunkCount = 1040;
    public static int chunkSize = 500;

    public void Start()
    {
        chunkToSpawn = chunk;
        chunkTypes = placeHolder;
        chunks.Add(Instantiate(chunkToSpawn));
        SpawnChunk(chunkToSpawn.transform);
    }

    public static void SpawnChunk(Transform pos)
    {
        Vector3[] directions = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, -1),
            new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(0, 1, -1),
            new Vector3(1, 1, 1),
            new Vector3(1, 1, -1),
            new Vector3(-1, 1, 1),
            new Vector3(-1, 1, -1),
            new Vector3(0, 1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, -1, 0),
            new Vector3(0, -1, 1),
            new Vector3(0, -1, -1),
            new Vector3(1, -1, 1),
            new Vector3(1, -1, -1),
            new Vector3(-1, -1, 1),
            new Vector3(-1, -1, -1),
            new Vector3(0, -1, 0)
        };

        List<GameObject> newChunks = new List<GameObject>();


        for (int i = 0; i < directions.Length; i++)
        {
            Vector3 newPosition = pos.position + directions[i] * chunkSize;
            if (!IsChunkAtPosition(newPosition))
            {
                GameObject newChunk = SpawnNewChunkAtPosition(newPosition);
                newChunks.Add(newChunk);
            }
        }

        foreach (GameObject chunk in newChunks)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                Vector3 newPosition = chunk.transform.position + directions[i] * chunkSize;
                if (!IsChunkAtPosition(newPosition))
                {
                    SpawnNewChunkAtPosition(newPosition);
                }
            }
        }
    }

    private static GameObject SpawnNewChunkAtPosition(Vector3 newPosition)
    {
        float chunkTypeAmount = 0;
        if (chunkTypeAmount <= 0)
        {
            chunkTypeAmount = Random.Range(100, 500);
            chunk lastChunk = currentChunk;
            currentChunk = chunkTypes[Random.Range(0, chunkTypes.Count)];
            if (currentChunk.name == lastChunk.name)
            {
                currentChunk = chunkTypes[Random.Range(0, chunkTypes.Count)];
            }
        }
        GameObject newChunk = Instantiate(chunkToSpawn, newPosition, Quaternion.identity);
        chunkTypeAmount -= 1;
        chunks.Add(newChunk);
        chunkCounter++;
        newChunk.GetComponent<ChunkScript>().chunkCount = chunkCounter;
        newChunk.GetComponent<ChunkScript>().chunk = currentChunk;
        if (chunks.Count > maxChunkCount)
        {
            for (int j = 0; j < 9; j++)
            {
                Destroy(chunks[j]);
                chunks.RemoveAt(j);
            }
        }
        return newChunk;
    }

    public static bool IsChunkAtPosition(Vector3 position)
    {
        foreach (GameObject existingChunk in chunks)
        {
            if (existingChunk.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }
}
