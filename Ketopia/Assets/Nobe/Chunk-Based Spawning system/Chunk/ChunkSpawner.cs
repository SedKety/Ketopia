using System.Collections;
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

    public static int maxChunkCount = 104;
    public static int chunkSize = 1000;
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
            new Vector3(-1, 0, -1) 
        };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector3 newPosition = pos.position + directions[i] * chunkSize;
            if (!IsChunkAtPosition(newPosition))
            {
                float chunkTypeAmount = 0;
                if (chunkTypeAmount <= 0)
                {
                    chunkTypeAmount = Random.Range(0, 4);
                    chunkTypeAmount *= 90;
                    chunk lastChunk = currentChunk;
                    currentChunk = chunkTypes[Random.Range(0, chunkTypes.Count)];
                    if(currentChunk.name == lastChunk.name)
                    {
                        currentChunk = chunkTypes[Random.Range(0, chunkTypes.Count)];
                    }
                }
                GameObject newChunk = Instantiate(chunkToSpawn, newPosition, Quaternion.identity);
                chunkTypeAmount -= 1;
                chunks.Add(newChunk);
                chunkCounter++;
                newChunk.GetComponent<ChunkScript>().chunk = currentChunk;
                newChunk.GetComponent<ChunkScript>().chunkCount = chunkCounter;
                if (chunks.Count > maxChunkCount)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        Destroy(chunks[j]);
                        chunks.Remove(chunks[j]);
                    }
                }
            }
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
