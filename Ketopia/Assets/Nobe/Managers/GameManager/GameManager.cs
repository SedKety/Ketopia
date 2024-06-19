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
    public int fps;
    public TMP_Dropdown menu;
    public void Start()
    {
        instance = this;
        ChangeFPS();
        LoadFPS();
        menu.value = PlayerPrefs.GetInt("FuckingGay");
    }
    public void SpawnCanyonIsland()
    {
        canyonActive = false;
        Instantiate(canyonIsland, spawnPosition, Quaternion.identity);
    }

    public void ChangeFPS()
    {
        fps =  int.Parse(menu.options[menu.value].text);
        print(fps);
        Application.targetFrameRate = fps;
        PlayerPrefs.SetInt("CurrentFPS", fps);
        PlayerPrefs.SetInt("FuckingGay", menu.value);
    }
    public void LoadFPS()
    {
        PlayerPrefs.GetInt("CurrentFPS", fps);
    }
}
