using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSpawnerScript : ResourceNodeSpawner
{
    public override void SpawnObjects()
    {
        if (spawnableGameobjects != null)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int retries = 0;
                RaycastHit hit;
                while (retries < maxRetries)
                {
                    transform.Rotate(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                    if (Physics.Raycast(transform.position, Vector3.forward, out hit, 50))
                    {
                        if (hit.collider.transform == transform.parent)
                        {
                            GameObject spawnedObject = Instantiate(spawnableGameobjects[Random.Range(0, spawnableGameobjects.Length)], hit.point, Quaternion.identity);
                            spawnedObject.transform.parent = transform;
                            spawnedObject.transform.up = hit.normal;
                            break;
                        }
                    }
                    retries++;
                    if (retries >= maxRetries)
                    {
                        break;
                    }
                }
            }
        }
        if (transform.childCount <= 10)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
