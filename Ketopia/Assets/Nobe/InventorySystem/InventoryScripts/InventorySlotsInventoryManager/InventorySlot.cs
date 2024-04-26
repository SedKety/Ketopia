using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Item item;
    public RawImage itemImage;
    public TextMeshProUGUI quantityText;


    public void OnItemAdd(Item item)
    {
        this.item = item;
        itemImage.gameObject.SetActive(true);
        itemImage.texture = item.itemSprite;
        quantityText.gameObject.SetActive(true);
        quantityText.text = item.quantity.ToString();
    }

    public void Update()
    {
        if(quantityText.isActiveAndEnabled) { quantityText.text = item.quantity.ToString(); } 
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("hello");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("bye");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("oi");
    }
}
