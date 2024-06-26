using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    public Vector3 spawnPosition;
    public GameObject canyonIsland;
    public bool canyonActive;
    public void Start()
    {
        instance = this;

    }
    public void SpawnCanyonIsland()
    {
        canyonActive = false;
        Instantiate(canyonIsland, spawnPosition, Quaternion.identity);
    }
}
