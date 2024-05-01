using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    public GameObject itemImage;
    public TextMeshProUGUI quantityText;

    public Vector3 originalPosition;

    public void Start()
    {
        originalPosition = transform.position;
    }
    public void OnItemAdd(Item item)
    {
        this.item = item;
        //itemImage.gameObject.SetActive(true);
        Invoke(nameof(EnableItemImage), 0.01f);
        quantityText.gameObject.SetActive(true);
        quantityText.text = item.quantity.ToString();
    }
    public void EnableItemImage()
    {
        itemImage.gameObject.SetActive(true);
        itemImage.GetComponent<RawImage>().texture = item.itemSprite;

    }
    public void OnItemRemove()
    {
        item = null;
        itemImage.gameObject.SetActive(false);
        itemImage.GetComponent<RawImage>().texture = null;
        quantityText.gameObject.SetActive(false);
        quantityText.text = null;
    }

    public void Update()
    {
        if (quantityText.isActiveAndEnabled) { quantityText.text = item.quantity.ToString(); }
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
        if(endDraggedObject != null )
        {
            print(endDraggedObject.name);
            if (endDraggedObject.GetComponent<InventorySlot>())
            {
                if (endDraggedObject.GetComponent<InventorySlot>().item == null)
                {
                    endDraggedObject.GetComponent<InventorySlot>().OnItemAdd(item);
                    OnItemRemove();
                }
            }
        }
        itemImage.GetComponent<RawImage>().raycastTarget = true;
    }
}
