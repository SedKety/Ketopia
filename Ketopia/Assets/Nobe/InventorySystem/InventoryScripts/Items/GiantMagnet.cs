using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMagnet : PhysicalItemScript
{
    string displayText;
    public override void IInteractable()
    {
        if(!GameManger.instance.canyonActive)
        {
            GameManger.instance.SpawnCanyonIsland();
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
        GameManger.instance.SpawnCanyonIsland();
        UIScript.instance.DisplayText(displayText);
    }
}
