using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public string dishName; // Nome do prato
    public SO_Ingredient[] ingredients; // Lista dos ingredientes necessários
    public int cookingTime; // Tempo do preparo
    public Sprite icon;
}