using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devtools : MonoBehaviour
{
    public bool devToolsOn;
    public GameObject[] devToolItems;

    public Chunk devChunk;
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            devToolsOn = true;
            print("on"); 
        }
        if (devToolsOn = true & Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < devToolItems.Length; i++)
            {
                ChunkSpawner.currentChunk = devChunk;
                ChunkSpawner.chunkTypeAmount += 200;
              var spawnedItem =  Instantiate(devToolItems[i], PlayerManager.instance.dropSpot.position, Quaternion.identity);
                spawnedItem.GetComponent<PhysicalItemScript>().quantity = spawnedItem.GetComponent<PhysicalItemScript>().item.maxQuantity;

            }
        }
    }
}
