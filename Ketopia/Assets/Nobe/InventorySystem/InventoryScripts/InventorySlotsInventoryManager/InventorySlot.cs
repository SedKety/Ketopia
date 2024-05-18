using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour //IBeginDragHandler, //IEndDragHandler, //IDragHandler, //IPointerClickHandler
{
    public Item item;
    public GameObject itemImage;
    public TextMeshProUGUI quantityText;
    public Button dropButton;

    public Vector3 originalPosition;

    public int quantity;
    public int maxQuantity;

    public void Start()
    {
        originalPosition = transform.position;
    }
    public void OnEnable()
    {
        if (originalPosition != null)
        {
            itemImage.gameObject.transform.position = originalPosition;
            itemImage.GetComponent<Image>().raycastTarget = true;
        }
        if (item & itemImage.GetComponent<Image>().sprite == null)
        {
            itemImage.GetComponent<Image>().sprite = item.itemSprite;
        }
    }
    public void Update()
    {
        if(quantity != 0)
        {
            quantityText.text = quantity.ToString();
        }
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
        }
    }
    public void OnItemDrop()
    {
        Instantiate(item.physicalItem, PlayerManager.instance.dropSpot.position, Quaternion.identity);
        OnItemRemove();
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
            return quantity;
        }
    }

    //public void Update()
    //{
    //    if (quantityText.isActiveAndEnabled && item != null)
    //    {
    //        quantityText.text = quantity.ToString();
    //    }
    //}
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    print("gudshit");
    //    itemImage.GetComponent<Image>().raycastTarget = false;
    //    itemImage.transform.SetAsLastSibling();
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    itemImage.transform.position = eventData.position;
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    itemImage.gameObject.transform.position = originalPosition;
    //    itemImage.transform.SetAsFirstSibling();

    //    GameObject endDraggedObject = eventData.pointerCurrentRaycast.gameObject;
    //    if (endDraggedObject != null)
    //    {
    //        if (endDraggedObject.GetComponent<InventorySlot>())
    //        {
    //            if (endDraggedObject.GetComponent<InventorySlot>().item == null)
    //            {
    //                endDraggedObject.GetComponent<InventorySlot>().OnItemAdd(item, quantity);
    //                OnItemRemove();
    //            }
    //            else if (endDraggedObject.gameObject.GetComponent<InventorySlot>().item == item)
    //            {
    //                int leftOverItems = endDraggedObject.gameObject.GetComponent<InventorySlot>().CalculateLeftOverSpace(quantity);
    //                if (leftOverItems <= 0)
    //                {
    //                    OnItemRemove();
    //                }
    //                else
    //                {
    //                    quantity = leftOverItems;
    //                }
    //            }
    //        }
    //        else if (endDraggedObject.CompareTag("DropZone") & item)
    //        {
    //            var spawnedItem = Instantiate(item.physicalItem, PlayerManager.instance.dropSpot.position, Quaternion.identity);
    //            spawnedItem.GetComponent<PhysicalItemScript>().quantity = quantity;
    //            OnItemRemove();
    //        }
    //    }
    //    itemImage.GetComponent<RawImage>().raycastTarget = true;
    //}

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    print("gudshit");
    //}
}
