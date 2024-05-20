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
    public void OnItemSwitch(Item item)
    {
        heldItem = item;
    }
    public void Update()
    {
        if(heldItem == null)
        {
            heldItem = fist;
            objectInHand = null;
        }
        if (Input.GetMouseButton(0) & canHit)
        {
            HoldableItemScript harvestingTool = heldItem as HoldableItemScript;
            if (harvestingTool)
            {
                canHit = false;
                StartCoroutine(HitCooldown());
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, hitRange))
                {
                    if (hit.collider.gameObject.GetComponent<ResourceNode>() != null)
                    {
                        hit.collider.gameObject.GetComponent<IDamagable>().IDamagable(objectInHandDmg, harvestingTool.typeToHarvest, harvestingTool.toolStrength);
                    }
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
