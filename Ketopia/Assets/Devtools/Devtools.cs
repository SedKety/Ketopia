using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devtools : MonoBehaviour
{
    public bool devToolsOn;

    public bool islandsOn;
    public GameObject allIslands;
    public Vector3 spawnPosition;

    public GameObject allItems;
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            devToolsOn = true;
            print("on"); 
        }
        if (devToolsOn = true & Input.GetKeyDown(KeyCode.F1))
        {
            Instantiate(allItems, PlayerManager.instance.dropSpot.position, Quaternion.identity);
            if (!islandsOn)
            {
                islandsOn = true;
                Instantiate(allIslands, spawnPosition, Quaternion.identity);
                ChunkSpawner.AnnihilateChunks();
                AirshipMovement.instance.airshipMovementSpeed *= 3;
            }
        }
    }
}
