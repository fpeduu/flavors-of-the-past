using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Ingredient
{
    public SO_Ingredient ingredientInfo;
    public int amount;
}

public class CookingManager : MonoBehaviour
{
    [Header("References")]
    public Button ingredientButtonPrefab;
    public GridLayoutGroup gridLayout;   // Referência ao GridLayoutGroup
    public RecipeBook recipeBook;
    public TextMeshProUGUI dishNameText;
    
    [Space(10)]
    public List<Ingredient> availableIngredients;
    
    
    private List<Button> selectedButtons = new List<Button>();  // Armazena os botões selecionados
    public List<Ingredient> selectedIngredients = new();
    private int _maxSelection = 5;  // Número máximo de ingredientes que podem ser selecionados

    private void Start()
    {
        FillGridWithButtons();
    }
    
    void FillGridWithButtons()
    {
        foreach (var ingredient in availableIngredients)
        {
            // Cria o botão
            Button newButton = Instantiate(ingredientButtonPrefab, gridLayout.transform);

            // Configura o texto do botão
            var buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = ingredient.ingredientInfo.ingredientName;

            // Configura o sprite do botão
            Image buttonImage = newButton.GetComponentInChildren<Image>();
            buttonImage.sprite = ingredient.ingredientInfo.icon;
            
            // Adiciona um listener para o clique do botão
            newButton.onClick.AddListener(() => OnButtonClicked(newButton, ingredient));
        }
    }
    
    void OnButtonClicked(Button clickedButton, Ingredient ingredient)
    {
        // Se o botão já está selecionado, desmarque-o
        if (selectedButtons.Contains(clickedButton))
        {
            DeselectButton(clickedButton, ingredient);
        }
        else
        {
            // Se ainda não foi selecionado e há espaço, selecione
            if (selectedButtons.Count < _maxSelection)
            {
                SelectButton(clickedButton, ingredient);
            }
            else
            {
                Debug.Log("Limite de seleção atingido.");
            }
        }
    }

    void SelectButton(Button button, Ingredient ingredient)
    {
        selectedButtons.Add(button);
        button.GetComponent<Image>().color = Color.green;  // Exemplo: Mudar a cor do botão para verde

        // Desmarcar os outros botões, se necessário
        UpdateButtonStates();
        
        selectedIngredients.Add(ingredient);
    }

    void DeselectButton(Button button, Ingredient ingredient)
    {
        selectedButtons.Remove(button);
        button.GetComponent<Image>().color = Color.white;  // Reseta a cor para o padrão

        UpdateButtonStates();
        
        selectedIngredients.Remove(ingredient);

    }

    void UpdateButtonStates()
    {
        foreach (Button btn in gridLayout.GetComponentsInChildren<Button>())
        {
            if (selectedButtons.Contains(btn))
            {
                // Mantém a cor dos botões selecionados
                btn.GetComponent<Image>().color = Color.green;
            }
            else
            {
                // Reseta a cor dos botões não selecionados
                btn.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void TryRecipe()
    {
        List<SO_Ingredient> ingredients = new();

        foreach (var ingredient in selectedIngredients)
        {
            ingredients.Add(ingredient.ingredientInfo);
        }

        var recipe = recipeBook.GetRecipe(ingredients);
        
        dishNameText.text = recipe ? "Você fez "+ recipe.dishName : "Tente com outros ingredientes!";
        Debug.Log(recipe?recipe.dishName:"Receita não encontrada");
    }
}


