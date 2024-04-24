using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandDespawner : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Island"))
        {
            Destroy(other.gameObject);
        }
    }
}
