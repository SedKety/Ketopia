using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devtools : MonoBehaviour
{
    public bool devToolsOn;
    public GameObject[] devToolItems;

    public GameObject allItems;
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
            Instantiate(allItems, PlayerManager.instance.dropSpot.position, Quaternion.identity);
        }
    }
}
