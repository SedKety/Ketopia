using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
