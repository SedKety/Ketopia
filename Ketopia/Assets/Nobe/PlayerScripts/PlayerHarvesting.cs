using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour
{
    public Item heldItem;
    public GameObject objectInHand;
    public Item fist;
    public int hitTime;
    public int hitRange;
    public int objectInHandDmg;
    public bool canHit;
    public void Start()
    {
        canHit = true;
        if(objectInHand == null)
        {
            objectInHand = gameObject;
        }
    }
    public void Update()
    {
        if(heldItem == null)
        {
            heldItem = fist;
        }
        if (Input.GetMouseButton(0) & canHit)
        {
            canHit = false;
            StartCoroutine(HitCooldown());
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, hitRange))
            {
                if (hit.collider.gameObject.GetComponent<ResourceNode>() != null)
                {
                    hit.collider.gameObject.GetComponent<IDamagable>().IDamagable(objectInHandDmg, objectInHand);
                }
            }
        }
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(hitTime);
        canHit = true;
    }
}
