using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public RecipeData[] recipes;  // Array de receitas (preenchido no Inspector)
    public GameObject buttonPrefab;  // Prefab do botão
    public Transform buttonContainer; // Onde os botões serão instanciados

    private void Start()
    {
        GenerateRecipeButtons();
    }

    private void GenerateRecipeButtons()
    {
        foreach (RecipeData recipe in recipes)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = recipe.recipeName; // Nome da receita
            button.GetComponent<Image>().sprite = recipe.recipeImage; // Imagem da receita
            
            // Configura o botão para carregar a cena correspondente
            button.GetComponent<Button>().onClick.AddListener(() => LoadRecipeScene(recipe.sceneName));
        }
    }

    private static void LoadRecipeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}