using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour
{
    public Item heldItem;
    public Item fist;
    public GameObject[] devToolItems;
    public int hitTime;
    public int hitRange;
    public int objectInHandDmg;
    public bool canHit;
    public bool defTools;
    public GameObject hand;
    public Animator animator;
    public void Start()
    {
        canHit = true;
        defTools = true;
    }
    public void OnItemSwitch(Item item)
    {
        for (int i = 0; i < hand.transform.childCount; i++)
        {
            if (hand.transform.GetChild(i).gameObject)
            {
                Destroy(hand.transform.GetChild(i).gameObject);
            }
        }
        heldItem = item;
        var itemInHand = Instantiate(heldItem.physicalItem, hand.transform.position, hand.transform.rotation, hand.transform);
        Destroy(itemInHand.GetComponent<Rigidbody>());
        Destroy(itemInHand.GetComponent<PhysicalItemScript>());
        Destroy(itemInHand.GetComponent<Collider>());
    }
    public void Update()
    {
        if (heldItem == null)
        {
            heldItem = fist;
            if(hand.transform.childCount != 0)
            {
                for (int i = 0; i < hand.transform.childCount; i++)
                {
                    if (hand.transform.GetChild(i).gameObject)
                    {
                        Destroy(hand.transform.GetChild(i).gameObject);
                    }
                }
            }
        }
        if (Input.GetMouseButton(0) & canHit)
        {
            HoldableItemScript harvestingTool = heldItem as HoldableItemScript;
            if (harvestingTool)
            {
                canHit = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                StartCoroutine(HitCooldown());
                if (Physics.Raycast(ray, out RaycastHit hit, hitRange))
                {
                    if (hit.collider.gameObject.GetComponent<IDamagable>() != null)
                    {
                        animator.SetBool("Swing", true); 
                        hit.collider.gameObject.GetComponent<IDamagable>().IDamagable(objectInHandDmg, harvestingTool.typeToHarvest, harvestingTool.toolStrength);
                        hit.collider.gameObject.GetComponent<AudioSource>().Play(0);
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
