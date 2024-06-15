using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static List<Crafter> crafters = new List<Crafter>();
    public static List<RecipeItemScript> recipes = new List<RecipeItemScript>();
    public static void OnRecipeAdd(RecipeItemScript recipeItemScript)
    {
        bool hasAlreadyBeenUsed = false;
        for(int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i] == recipeItemScript)
            {
                hasAlreadyBeenUsed = true;
            }
        }
        if (hasAlreadyBeenUsed == false)
        {
            recipes.Add(recipeItemScript);
            foreach (Crafter crafter in crafters)
            {
                if (crafter.type == recipeItemScript.type & !crafter.recipes.Contains(recipeItemScript.heldRecipe))
                {
                    crafter.recipes.Add(recipeItemScript.heldRecipe);
                }
            }
        }
    }
}
