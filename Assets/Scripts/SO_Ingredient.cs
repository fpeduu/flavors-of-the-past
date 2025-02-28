using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Cooking/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public string ingredientName; // Nome do ingrediente
    public IngredientType type; // Tipo (vegetal, carne, tempero, etc..)
    public Sprite icon;
}

public enum IngredientType
{
    Carne,
    Vegetal,
    Fruta,
    Peixe,
    Tempero,
    Erva,
    Cogumelo,
    Venenoso,
    Grão,
    Laticínio,
    Adoçante,
    Líquido
}