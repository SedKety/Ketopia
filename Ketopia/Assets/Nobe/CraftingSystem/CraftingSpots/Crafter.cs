using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CrafterType
{
    craftingBench,
    forge,
    cooker,
}
public class Crafter : MonoBehaviour, IInteractable
{
    public CrafterType type;
    public List<Recipe> recipes;
    public List<Recipe> possibleRecipe;
    public List<CrafterInput> crafterInputs;

    public Transform outputItemSpot;

    public void Start()
    {
        RecipeManager.crafters.Add(this);
    }
    public void CraftItem()
    {
        possibleRecipe.Clear();

        PhysicalItemScript inputItem = crafterInputs[0].currentItem;
        PhysicalItemScript inputItem1 = crafterInputs[1].currentItem;

        if (inputItem == null || inputItem1 == null)
        {
            Debug.Log("Er mist een of meer van de items");
            return;
        }

        foreach (Recipe recipe in recipes)
        {
            bool matchOriginalOrder = recipe.inputItem1 == inputItem.item && recipe.inputItem1Amount <= inputItem.quantity &&
                                      recipe.inputItem2 == inputItem1.item && recipe.inputItem2Amount <= inputItem1.quantity;
            bool matchSwitchedOrder = recipe.inputItem1 == inputItem1.item && recipe.inputItem1Amount <= inputItem1.quantity &&
                                      recipe.inputItem2 == inputItem.item && recipe.inputItem2Amount <= inputItem.quantity;

            if (matchOriginalOrder || matchSwitchedOrder)
            {
                possibleRecipe.Add(recipe);
            }
        }

        if (possibleRecipe.Count == 1)
        {
            Recipe finalRecipe = possibleRecipe[0];
            Debug.Log(finalRecipe.outputItem.name);

            if (finalRecipe.inputItem1 == inputItem.item)
            {
                inputItem.UpdateQuantity(finalRecipe.inputItem1Amount);
                inputItem1.UpdateQuantity(finalRecipe.inputItem2Amount);
            }
            else
            {
                inputItem.UpdateQuantity(finalRecipe.inputItem2Amount);
                inputItem1.UpdateQuantity(finalRecipe.inputItem1Amount);
            }

            var craftedItem = Instantiate(finalRecipe.outputItem, outputItemSpot.position, Quaternion.identity);
            var craftedItemScript = craftedItem.GetComponent<PhysicalItemScript>();
            if (craftedItemScript != null)
            {
                craftedItemScript.quantity = finalRecipe.outputItemAmount;
            }
        }
        else
        {
            Debug.Log("Nuh Uh, couldnt find anything");
        }
    }

    public void IInteractable()
    {
        CraftItem();
    }
}
