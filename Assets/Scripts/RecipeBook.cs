using UnityEngine;
using System.Collections.Generic;
public class RecipeBook : MonoBehaviour
{
    public Recipe[] recipes;

    public Recipe GetRecipe(List<SO_Ingredient> providedIngredients)
    {
        foreach (var recipe in recipes)
        {
            if (MatchesIngredients(recipe, providedIngredients))
            {
                return recipe;
            }
        }

        return null; // Retorna null se a combinação não existir
    }

    private static bool MatchesIngredients(Recipe recipe, List<SO_Ingredient> providedIngredients)
    {
        if (providedIngredients.Count != recipe.ingredients.Length) return false;

        foreach (SO_Ingredient ingredient in recipe.ingredients)
        {
            bool found = false;
            foreach (SO_Ingredient provided in providedIngredients)
            {
                if (provided == ingredient)
                {
                    found = true;
                    break;
                }
            }
            if (!found) return false;
        }

        return true;
    }
}
