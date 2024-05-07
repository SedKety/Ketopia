using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ZuigbuisScript : MonoBehaviour
{
    public Item item;
    public GameObject itemImage;
    public TextMeshProUGUI quantityText;

    public int maxQuantity;
    public int quantity;
    public int CalculateLeftOverSpace(int quantity)
    {
        var leftOverSpace = maxQuantity - quantity;
        if (leftOverSpace >= quantity)
        {
            quantity += quantity;
            return 0;
        }
        else
        {
            quantity += leftOverSpace;
            quantity -= leftOverSpace;
            return quantity;
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
            quantityText.text = quantity.ToString();
        }
    }
}
