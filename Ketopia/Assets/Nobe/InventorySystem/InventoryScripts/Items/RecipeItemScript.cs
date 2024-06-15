using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/RecipeItem")]
public class RecipeItemScript : Item
{
    public Recipe heldRecipe;
    public CrafterType type;

    public override void OnItemUse()
    {
        RecipeManager.OnRecipeAdd(this);
        UIScript.instance.DisplayText("The Scroll reads:  To craft the magnet you shall craft 5 magnets, and make 10 magnetite bars, combine these and behold the power. Of the magnet");
    }
}
