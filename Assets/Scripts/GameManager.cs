using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] miniGames; // Lista de Mini-Games (Painéis ou GameObjects)
    public TextMeshProUGUI instructionText;
    
    private int currentStep = 0;

    public GameObject nextButton;

    void Start()
    {
        StartRecipe();
    }

    void StartRecipe()
    {
        currentStep = 0;
        ShowMiniGame(currentStep);
    }

    void ShowMiniGame(int stepIndex)
    {
        // Desativa todos os mini-games
        foreach (GameObject game in miniGames)
        {
            game.SetActive(false);
        }

        if (stepIndex < miniGames.Length)
        {
            // Ativa o mini-game atual
            miniGames[stepIndex].SetActive(true);
            
            // Atualiza a instrução na tela
            instructionText.text = GetInstructionForStep(stepIndex);

        }
        else
        {
            // Se acabou a receita, mostra a tela final
            instructionText.text = "Receita concluída!";
            Debug.Log("Jogo Finalizado!");
        }
    }

    public void ShowNextButton(bool show)
    {
        nextButton.SetActive(show);
    }

    public void CompleteMiniGame()
    {
        currentStep++;
        ShowMiniGame(currentStep);
        // ShowNextButton(false);
    }
    
    string GetInstructionForStep(int step)
    {
        switch (step)
        {
            case 0: return "Hora de cortar o pão!";
            case 1: return "Asse a carne!";
            case 2: return "Hora de montar!";
            default: return "Finalizando...";
        }
    }
}