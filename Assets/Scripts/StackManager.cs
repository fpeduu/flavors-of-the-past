using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public Transform plate; // O prato fixo na cena
    public GameObject[] ingredientPrefabs; // Ordem fixa dos ingredientes (pão, carne, alface, tomate, pão)
    public Transform spawnPosition;
    private Stack<Transform> ingredientStack = new Stack<Transform>();
    private int currentIndex = 0; // Para garantir que a ordem seja seguida

    public float allowedOffset = 1.5f;

    void Start()
    {
        // Inicia a pilha com o prato fixo
        ingredientStack.Push(plate);
        SpawnNextIngredient();
    }

    void SpawnNextIngredient()
    {
        if (currentIndex < ingredientPrefabs.Length)
        {
            Instantiate(ingredientPrefabs[currentIndex], spawnPosition.position, Quaternion.identity, transform);
            currentIndex++;
        }
        else
        {
            FinishMinigame();
        }
    }

    public void CheckPlacement(Transform ingredient)
    {
        Transform lastItem = ingredientStack.Peek();
        
        // Verifica se o ingrediente caiu em um lugar no eixo y
        if (ingredient.position.y <= lastItem.position.y)
        {
            Debug.Log($"y do ingredient: {ingredient.position.y}, y do lastItem: {lastItem.position.y}");

            GameOver();
            return;
        }
        
        // Verifica se o ingrediente caiu em um lugar no eixo x
        float offset = ingredient.position.x - lastItem.position.x;

        if (Mathf.Abs(offset) > allowedOffset)
        {
            GameOver();
        }
        else
        {
            Debug.Log("O ingrediente caiu certo!");
            StackIngredient(ingredient);
        }
    }

    public void StackIngredient(Transform ingredient)
    {
        // Adiciona o ingrediente no topo da pilha
        ingredientStack.Push(ingredient);

        SpawnNextIngredient();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }

    public void FinishMinigame()
    {
        GameManager.Instance.ShowNextButton(true);
    }
}