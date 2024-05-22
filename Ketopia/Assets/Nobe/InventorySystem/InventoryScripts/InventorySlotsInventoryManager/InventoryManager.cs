using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvState
{
    normal,
    drop,
}
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventory;
    public GameObject inventorySlotsManager;
    public InvState inventoryState;
    public PlayerState playerState;
    public InventorySlot[] inventorySlots;

    public void Start()
    {
        instance = this;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) & !inventory.activeInHierarchy & PlayerManager.instance.playerState != PlayerState.ship & PlayerManager.instance.playerState != PlayerState.dialogue & PlayerManager.instance.playerState != PlayerState.menu)
        {
            playerState = PlayerManager.instance.playerState;
            PlayerManager.instance.SwitchState(PlayerState.inventory);
            inventory.SetActive(true);
            SwitchState(InvState.normal);
        }
        else if (Input.GetKeyDown(KeyCode.E) & inventory.activeInHierarchy)
        {
            PlayerManager.instance.SwitchState(playerState);
            inventory.SetActive(false);
        }
    }
    public void EnableDropMode()
    {
        if(inventoryState == InvState.drop)
        {
            SwitchState(InvState.normal);
        }
        else if(inventoryState == InvState.normal)
        {
            SwitchState(InvState.drop);
        }
    }

    public int OnItemAdd(Item item, int quantity)
    {
        SwitchState(InvState.normal);
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item == item)
            {
                var leftOverSpace = inventorySlots[i].maxQuantity - inventorySlots[i].quantity;
                if (leftOverSpace >= quantity)
                {
                    inventorySlots[i].quantity += quantity;
                    return 0;
                }
                else
                {
                    inventorySlots[i].quantity += leftOverSpace;
                    quantity -= leftOverSpace;
                }
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item == null)
            {
                inventorySlots[i].OnItemAdd(item, quantity);
                inventorySlots[i].quantity = Mathf.Min(quantity, inventorySlots[i].maxQuantity);
                return 0;
            }
        }
        return quantity;
    }


    public void SwitchState(InvState state)
    {
        inventoryState = state;
        switch (state)
        {
            case InvState.drop:
                for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                {
                    if(inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item != null)
                    {
                        inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().dropButton.gameObject.SetActive(true);
                    }
                }
                break;
            case InvState.normal:
                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().dropButton.gameObject.SetActive(false);
                }
                break;
        }
    }
}
