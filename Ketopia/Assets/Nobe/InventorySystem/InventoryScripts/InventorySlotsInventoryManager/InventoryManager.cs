using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum InvState
{
    normal,
    drop,
    consume,
    equip,
}
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventory;
    public GameObject inventorySlotsManager;
    public InvState inventoryState;
    public PlayerState playerState;
    public InventorySlot[] inventorySlots;


    public Image heldItemImage;
    public Button unequipHeldItemButton;
    public PlayerHarvesting playerHarvesting;

    public bool dropMode;
    public bool consumeMode;
    public bool equipMode;
    public bool buildMode;

    public void Start()
    {
        instance = this;
        playerHarvesting = FindAnyObjectByType<PlayerHarvesting>();
    }
    public void EquipItem(Item item)
    {
        if (playerHarvesting.heldItem != null & playerHarvesting.heldItem != playerHarvesting.fist)
        {
            OnItemAdd(playerHarvesting.heldItem, 1);
        }
        playerHarvesting.heldItem = item;
        heldItemImage.gameObject.SetActive(true);
        heldItemImage.sprite = playerHarvesting.heldItem.itemSprite;
        playerHarvesting.heldItem = item;
        playerHarvesting.OnItemSwitch(item);
    }

    public void UnEquipItem()
    {
        if (playerHarvesting.heldItem != null & playerHarvesting.heldItem != playerHarvesting.fist)
        {
            heldItemImage.gameObject.SetActive(false);
            unequipHeldItemButton.gameObject.SetActive(false);
            heldItemImage.sprite = null;
            OnItemAdd(playerHarvesting.heldItem, 1);
            playerHarvesting.heldItem = null;
        }
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

        SwitchState(InvState.drop);

    }
    public void EnableConsumeMode()
    {
        SwitchState(InvState.consume);

    }
    public void EnableEquipMode()
    {
        SwitchState(InvState.equip);

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
                if (equipMode & item.itemType == ItemType.holdable) { inventorySlots[i].equipButton.gameObject.SetActive(true); }
                if (consumeMode & item.itemType == ItemType.consumable) { inventorySlots[i].consumeButton.gameObject.SetActive(true); }
                if (dropMode) { inventorySlots[i].dropButton.gameObject.SetActive(true); }
                inventorySlots[i].quantity = Mathf.Min(quantity, inventorySlots[i].maxQuantity);
                return 0;
            }
        }
        return quantity;
    }


    public void SwitchState(InvState state)
    {
        inventoryState = state;
        unequipHeldItemButton.gameObject.SetActive(false);

        switch (state)
        {

            //DropMode
            case InvState.drop:
                if (dropMode == false)
                {
                    dropMode = true;
                    for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                    {
                        if (inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item != null)
                        {
                            inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().dropButton.gameObject.SetActive(true);
                        }
                    }
                }
                else if (dropMode == true)
                {
                    for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                    {
                        inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().dropButton.gameObject.SetActive(false);
                    }
                    dropMode = false;
                }
                break;

            //ConsumeMode
            case InvState.consume:
                if (consumeMode == false)
                {
                    consumeMode = true;
                    for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                    {
                        if (inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item != null)
                        {
                            if (inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item.itemType == ItemType.consumable || inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item.itemType == ItemType.recipeItem)
                            {
                                inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().consumeButton.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                else if (consumeMode == true)
                {
                    consumeMode = false;
                    for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                    {
                        inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().consumeButton.gameObject.SetActive(false);
                    }
                }
                break;

            //EquipMode
            case InvState.equip:
                if (equipMode == false)
                {
                    equipMode = true;
                    if (playerHarvesting.heldItem != null & playerHarvesting.heldItem != playerHarvesting.fist)
                    {
                        unequipHeldItemButton.gameObject.SetActive(true);
                    }
                    for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                    {
                        if (inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item != null)
                        {
                            if (inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().item.itemType == ItemType.holdable)
                            {
                                inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().equipButton.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                else if (equipMode == true)
                {
                    equipMode = false;
                    if (playerHarvesting.heldItem != null & playerHarvesting.heldItem != playerHarvesting.fist)
                    {
                        unequipHeldItemButton.gameObject.SetActive(false);
                    }
                    for (int i = 0; i < inventorySlotsManager.transform.childCount; i++)
                    {
                        inventorySlotsManager.transform.GetChild(i).GetComponent<InventorySlot>().equipButton.gameObject.SetActive(false);
                    }
                }
                break;
        }
    }
}
