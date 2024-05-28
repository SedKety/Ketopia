using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassIsland : ResourceNodeSpawner
{
    public bool spawnedGrass;
    public GameObject grass;
    public int grassCount;

    public GameObject[] flowers;
    public int flowerCount;
    public void Start()
    {
        spawnedGrass = false;
    }
    public override void SpawnObjects()
    {
        if (spawnedGrass == true)
        {
            base.SpawnObjects();
        }
        else
        {
            SpawnGrass();
        }
    }
    public void SpawnGrass()
    {

        if (spawnableGameobjects != null && spawnCollider != null)
        {
            for (int i = 0; i < grassCount; i++)
            {
                int retries = 0;
                Vector3 whereToSpawn;
                RaycastHit hit;
                while (retries < maxRetries)
                {
                    whereToSpawn = SpawnPosition(spawnCollider.bounds);
                    if (Physics.Raycast(whereToSpawn, Vector3.down, out hit, 50))
                    {
                        if (hit.collider.transform == transform.parent)
                        {
                            GameObject spawnedObject = Instantiate(grass, hit.point, Quaternion.identity);
                            spawnedObject.transform.Rotate(0, Random.Range(0, 360), 0);
                            spawnedObject.transform.parent = transform;
                            spawnedObjects.Add(spawnedObject);
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
            spawnedGrass = true;
            SpawnFlowers();
        }
    }
    public void SpawnFlowers()
    {
        for (int i = 0; i < flowerCount; i++)
        {
            int retries = 0;
            Vector3 whereToSpawn;
            RaycastHit hit;
            while (retries < maxRetries)
            {
                whereToSpawn = SpawnPosition(spawnCollider.bounds);
                if (Physics.Raycast(whereToSpawn, Vector3.down, out hit, 50))
                {
                    if (hit.collider.transform == transform.parent)
                    {
                        GameObject spawnedObject = Instantiate(flowers[Random.Range(0, flowers.Length)], hit.point, Quaternion.identity);
                        spawnedObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        spawnedObject.transform.parent = transform;
                        spawnedObjects.Add(spawnedObject);
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
        SpawnObjects();
    }
}
