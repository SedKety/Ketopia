using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    public GameObject itemImage;
    public TextMeshProUGUI quantityText;

    public Vector3 originalPosition;
    public Transform inventoryTransform;

    public void Start()
    {
        originalPosition = transform.position;
    }
    public void OnEnable()
    {
        if (originalPosition != null)
        {
            itemImage.gameObject.transform.position = originalPosition;
            itemImage.GetComponent<RawImage>().raycastTarget = true;
        }
        if (item & itemImage.GetComponent<RawImage>().texture == null)
        {
            itemImage.GetComponent<RawImage>().texture = item.itemSprite;
        }
    }
    public void OnItemAdd(Item item)
    {
        this.item = item;
        if (item != null)
        {
            itemImage.gameObject.SetActive(true);

            itemImage.GetComponent<RawImage>().texture = item.itemSprite;

            quantityText.gameObject.SetActive(true);
            quantityText.text = item.quantity.ToString();
        }
    }
    public void OnItemRemove()
    {
        item = null;
        itemImage.gameObject.SetActive(false);
        quantityText.text = null;
        itemImage.GetComponent<RawImage>().texture = null;
    }
    public int CalculateLeftOverSpace(int quantity)
    {
        var leftOverSpace = item.maxQuantity - quantity;
        if (leftOverSpace >= quantity)
        {
            item.quantity += quantity;
            return 0;
        }
        else
        {
            item.quantity += leftOverSpace;
            quantity -= leftOverSpace;
            return quantity;
        }
    }

    public void Update()
    {
        if (quantityText.isActiveAndEnabled && item != null)
        {
            quantityText.text = item.quantity.ToString();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.GetComponent<RawImage>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        itemImage.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.gameObject.transform.position = originalPosition;

        GameObject endDraggedObject = eventData.pointerCurrentRaycast.gameObject;
        if (endDraggedObject != null)
        {
            if (endDraggedObject.GetComponent<InventorySlot>())
            {
                if (endDraggedObject.GetComponent<InventorySlot>().item == null)
                {
                    endDraggedObject.GetComponent<InventorySlot>().OnItemAdd(item);
                    OnItemRemove();
                }
                else if (endDraggedObject.gameObject.GetComponent<InventorySlot>().item == item)
                {
                    int leftOverItems = endDraggedObject.gameObject.GetComponent<InventorySlot>().CalculateLeftOverSpace(item.quantity);
                    if (leftOverItems <= 0)
                    {
                        OnItemRemove();
                    }
                    else
                    {
                        item.quantity = leftOverItems;
                    }
                }
            }
            else if (endDraggedObject.CompareTag("DropZone"))
            {
                var spawnedItem = Instantiate(item.physicalItem, PlayerManager.instance.dropSpot.position, Quaternion.identity);
                spawnedItem.GetComponent<PhysicalItemScript>().quantity = item.quantity;
                OnItemRemove();
            }
        }
        itemImage.GetComponent<RawImage>().raycastTarget = true;
    }
}
