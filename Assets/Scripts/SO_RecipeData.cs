using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe Data")]
public class RecipeData : ScriptableObject
{
    public string recipeName;  // Nome da receita
    public Sprite recipeImage; // Imagem do prato
    public string sceneName;   // Nome da cena correspondente
}