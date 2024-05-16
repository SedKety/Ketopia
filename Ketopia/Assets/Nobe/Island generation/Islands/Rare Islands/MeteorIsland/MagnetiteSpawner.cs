using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetiteSpawner : MonoBehaviour
{
    public GameObject[] spawnableObjects;
    public int spawnCount;
    public LayerMask spawnMask;
    public void SpawnObjects()
    {
        gameObject.layer = 2;
        for (int i = 0; i < spawnCount; i++)
        {
            var spawnLocation = new Vector3(0, 0, 0);
            spawnLocation = Random.insideUnitCircle * 16;
            spawnLocation.z += transform.position.z;
            spawnLocation.z += Random.Range(-10, 10);
            spawnLocation.x += transform.position.x;
            spawnLocation.y += 50;
            print(spawnLocation);
            RaycastHit hit;
            if (Physics.Raycast(spawnLocation, Vector3.down, out hit))
            {
                var spawnedobject = Instantiate(spawnableObjects[Random.Range(0, 2)], hit.point, Random.rotation, transform);
                spawnedobject.transform.up = hit.normal;
            }
        }
    }
    public void Start()
    {
        Invoke("SpawnObjects", 0.25f);
    }
}
