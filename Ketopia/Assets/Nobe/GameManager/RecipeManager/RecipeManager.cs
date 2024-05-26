using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static List<Crafter> crafters = new List<Crafter>();
    public static void OnRecipeAdd(RecipeItemScript recipeItemScript)
    {
        foreach(Crafter crafter in crafters)
        {
            if(crafter.type == recipeItemScript.type & !crafter.recipes.Contains(recipeItemScript.heldRecipe))
            {
                crafter.recipes.Add(recipeItemScript.heldRecipe);
            }
        }
    }
}
