using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/RecipeItem")]
public class RecipeItemScript : Item
{
    public Recipe heldRecipe;
    public CrafterType type;

    public string displayString;
    public bool shouldDisplay;

    public override void OnItemUse()
    {
        RecipeManager.OnRecipeAdd(this);
        if(shouldDisplay)
        {
            UIScript.instance.DisplayText(displayString);
        }
    }
}
