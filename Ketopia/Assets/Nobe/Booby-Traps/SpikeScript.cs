using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public float dmgToDo;
    public SpikeTrap trap;
    public bool canDealDmg;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & canDealDmg)
        {
            other.GetComponent<IDamagable>().IDamagable(dmgToDo, NodeType.everything, 0);
            canDealDmg = false;
        }
    }
}
