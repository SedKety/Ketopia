using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour 
{
    public Item item;
    public GameObject itemImage;
    public TextMeshProUGUI quantityText;
    public Button dropButton;
    public Button consumeButton;
    public Button equipButton;
    public Button buildButton;
    public int quantity;
    public int maxQuantity;

    public void OnEnable()
    {
        quantityText.text = quantity.ToString();
        if (item & itemImage.GetComponent<Image>().sprite == null)
        {
            itemImage.GetComponent<Image>().sprite = item.itemSprite;
        }
    }
    public void UpdateQuantityTextValue()
    {
        quantityText.text = quantity.ToString();
    }
    public void OnItemAdd(Item item, int quantityToAdd)
    {
        this.item = item;
        if (item != null)
        {
            quantity += quantityToAdd;
            maxQuantity = item.maxQuantity;
            itemImage.gameObject.SetActive(true);

            itemImage.GetComponent<Image>().sprite = item.itemSprite;

            quantityText.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
    }
    public void OnItemDrop()
    {
        if(item != null)
        {
            var spawnedItem = Instantiate(item.physicalItem, PlayerManager.instance.dropSpot.position, Quaternion.identity);
            spawnedItem.GetComponent<PhysicalItemScript>().quantity = quantity;
            OnItemRemove();
        }
        dropButton.gameObject.SetActive(false);
    }
    public void OnItemEquip()
    {
        if(item != null)
        {
            if (item.itemType == ItemType.holdable & quantity == 1)
            {
                InventoryManager.instance.EquipItem(item);
                equipButton.gameObject.SetActive(false);
                OnItemRemove();
            }
        }
    }
    public void OnItemConsume()
    {
        item.OnItemUse();
        quantity -= 1;
        if(quantity <= 0)
        {
            OnItemRemove();
            consumeButton.gameObject.SetActive(false);
            quantityText.text = quantity.ToString();
        }
    }
    public void OnItemRemove()
    {
        item = null;
        itemImage.gameObject.SetActive(false);
        quantityText.text = null;
        itemImage.GetComponent<Image>().sprite = null;
        quantity = 0;

    }
    public int CalculateLeftOverSpace(int amountToCalculate)
    {
        var leftOverSpace = maxQuantity - quantity;
        if (leftOverSpace >= quantity)
        {
            amountToCalculate += quantity;
            return 0;
        }
        else
        {
            quantity -= leftOverSpace;
            quantityText.text = quantity.ToString();
            return quantity;
        }
    }

}
