using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour, IInteractable
{
    public List<Recipe> recipes;
    public List<Recipe> possibleRecipe;
    public List<CrafterInput> crafterInputs;
    public List<PhysicalItemScript> slot;

    public Transform outputItemSpot;
    public void CraftItem()
    {
        if (slot != null)
        {
            for (int i = 0; i < slot.Count; i++)
            {
                if (slot[i] == null)
                {
                    slot.Remove(slot[i]);
                }
            }
        }
        slot.Clear();
        possibleRecipe.Clear();
        for (int i = 0; i < crafterInputs.Count; i++)
        {
            slot.Add(crafterInputs[i].currentItem);
        }
        foreach (Recipe recipe in recipes)
        {
            if (slot[0])
            {
                if (recipe.inputItem1 == slot[0].item)
                {
                    possibleRecipe.Add(recipe);
                }
            }
        }
        foreach (Recipe recipe in recipes)
        {
            if (slot[1])
            {
                if (recipe.inputItem2 != slot[1].item)
                {
                    possibleRecipe.Remove(recipe);
                }
            }
        }
        if (possibleRecipe.Count == 1)
        {
            foreach (PhysicalItemScript slot in slot)
            {
                if (slot & slot.GetComponent<PhysicalItemScript>() != null)
                {
                    slot.UpdateQuantity(1);
                }

            }
            var craftedItem = Instantiate(possibleRecipe[0].outputItem, outputItemSpot.position, Quaternion.identity);
            craftedItem.GetComponent<PhysicalItemScript>().quantity = 1;
        }
        slot.Clear();
        possibleRecipe.Clear();
    }

    public void IInteractable()
    {
        CraftItem();
    }
}
