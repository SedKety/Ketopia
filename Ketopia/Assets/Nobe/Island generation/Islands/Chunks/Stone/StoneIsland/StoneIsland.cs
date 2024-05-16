using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneIsland : ResourceNodeSpawner
{
    public GameObject smallStone;
    public int smallStoneAmount;
    public GameObject mediumStone;
    public int mediumStoneAmount;
    public GameObject tree;
    public int treeAmount;
    public GameObject grass;
    public int grassAmount;

    public override void SpawnObjects()
    {
        for (int i = 0; i < smallStoneAmount; i++)
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
                        GameObject spawnedObject = Instantiate(smallStone, hit.point, Quaternion.identity);
                        spawnedObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        spawnedObject.transform.parent = transform;
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
        for (int i = 0; i < mediumStoneAmount; i++)
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
                        GameObject spawnedObject = Instantiate(mediumStone, hit.point, Quaternion.identity);
                        spawnedObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        spawnedObject.transform.parent = transform;
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
        for (int i = 0; i < treeAmount; i++)
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
                        GameObject spawnedObject = Instantiate(tree, hit.point, Quaternion.identity);
                        spawnedObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        spawnedObject.transform.parent = transform;
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

        for (int i = 0; i < grassAmount; i++)
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
}