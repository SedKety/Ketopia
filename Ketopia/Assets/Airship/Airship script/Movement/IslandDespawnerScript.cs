using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandDespawnerScript : MonoBehaviour
{
    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Island"))
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
