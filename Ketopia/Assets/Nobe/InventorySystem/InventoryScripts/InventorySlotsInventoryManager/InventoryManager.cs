using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventory;
    public PlayerState playerState;

    public GameObject inventorySlotObject;
    public InventorySlot[] inventorySlots;

    public void Start()
    {
        instance = this;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) & !inventory.activeInHierarchy & PlayerManager.instance.playerState != PlayerState.ship)
        {
            playerState = PlayerManager.instance.playerState;
            PlayerManager.instance.SwitchState(PlayerState.inventory);
            inventory.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) & inventory.activeInHierarchy)
        {
            PlayerManager.instance.SwitchState(playerState);
            inventory.SetActive(false);
        }
    }

    public int OnItemAdd(Item item, int quantity)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item == item)
            {
                var leftOverSpace = inventorySlots[i].item.maxQuantity - inventorySlots[i].item.quantity;
                if (leftOverSpace >= quantity)
                {
                    inventorySlots[i].item.quantity += quantity;
                    return 0; 
                }
                else
                {
                    inventorySlots[i].item.quantity += leftOverSpace;
                    quantity -= leftOverSpace;
                }
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item == null)
            {
                inventorySlots[i].OnItemAdd(item);
                var remainingSpace = item.maxQuantity - quantity;
                inventorySlots[i].item.quantity = Mathf.Min(quantity, remainingSpace);
                return 0; //niks meer dus return je niks. kleine L imo
            }
        }


        return quantity;
    }
}
