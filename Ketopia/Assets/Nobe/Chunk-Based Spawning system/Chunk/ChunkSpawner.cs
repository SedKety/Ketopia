using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public static ChunkSpawner instance;
    public GameObject chunk;
    public static GameObject chunkToSpawn;
    public static List<GameObject> chunks = new();
    public static int chunkCounter;

    public Chunk startChunk;
    public static Chunk currentChunk;
    public List<Chunk> placeHolder;
    public static List<Chunk> chunkTypes;

    public static int maxChunkCount = 1040;
    public static int chunkSize = 500;

    public static float chunkTypeAmount = 0;
    public void Start()
    {
        instance = this;
        currentChunk = startChunk;
        chunkTypeAmount = Random.Range(200, 400); chunkToSpawn = chunk;
        chunkTypes = placeHolder;
        chunks.Add(Instantiate(chunkToSpawn));
        SpawnChunk(chunkToSpawn.transform);
        chunkTypeAmount = Random.Range(200, 400);
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

        List<GameObject> newChunks = new();


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
        if (chunkTypeAmount <= 0)
        {
            chunkTypeAmount = Random.Range(200, 400);
            Chunk lastChunk = currentChunk;
            currentChunk = chunkTypes[Random.Range(0, chunkTypes.Count)];
            if (currentChunk.name == lastChunk.name)
            {
                currentChunk = chunkTypes[Random.Range(0, chunkTypes.Count)];
                print(currentChunk.name);
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

    public static void AnnihilateChunks()
    {
        print(chunks.Count);
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].gameObject.SetActive(false);
        }
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
