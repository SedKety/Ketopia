using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreClusterSpawner : MonoBehaviour
{
    public GameObject[] commonOres;
    public int howManyCommonOres;
    public GameObject[] rareOres;
    public int howManyRareOres;

    public Collider spawnCollider;
    public void SpawnOres()
    {
        for (int i = 0; i < howManyCommonOres; i++)
        {
            int retries = 0;
            Vector3 whereToSpawn;
            RaycastHit hit;
            while (retries < 10)
            {
                whereToSpawn = SpawnPosition(spawnCollider.bounds);
                whereToSpawn.y += 20;
                if (Physics.Raycast(whereToSpawn, Vector3.down, out hit, 50))
                {
                    if (hit.collider.transform == transform.parent)
                    {
                        GameObject spawnedObject = Instantiate(commonOres[Random.Range(0, commonOres.Length)], hit.point, Quaternion.identity);
                        spawnedObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        spawnedObject.transform.up = hit.normal;
                        spawnedObject.transform.parent = transform;
                        break;
                    }
                }
                retries++;
                if (retries >= 10)
                {
                    break;
                }
            }
        }
    }
    public virtual Vector3 SpawnPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
        float randomZ = Random.Range(bounds.center.z - bounds.extents.z, bounds.center.z + bounds.extents.z);

        return new Vector3(randomX, bounds.min.y, randomZ);
    }

}

