using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMagnet : PhysicalItemScript, IInteractable
{
    public override void IInteractable()
    {
        if(!GameManager.instance.canyonActive)
        {
            GameManager.instance.SpawnCanyonIsland();
        }
        int leftOverItems = InventoryManager.instance.OnItemAdd(item, quantity);
        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }
}
