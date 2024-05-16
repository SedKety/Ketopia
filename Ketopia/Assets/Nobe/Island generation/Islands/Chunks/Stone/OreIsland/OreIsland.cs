using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreIsland : MonoBehaviour
{
    public GameObject[] oreClusters;
    void Start()
    {
        Invoke("SpawnOresInClusters", 0.1f);
    }
    public void SpawnOresInClusters()
    {
        foreach (var oreCluster in oreClusters)
        {
            oreCluster.GetComponent<OreClusterSpawner>().SpawnOres();
        }
    }
}
