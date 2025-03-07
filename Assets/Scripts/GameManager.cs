using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] miniGames; // Lista de Mini-Games (Painéis ou GameObjects)

    private int currentStep = 0;

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

        }
        else
        {
            Debug.Log("Jogo Finalizado!");
        }
    }

    public void CompleteMiniGame()
    {
        currentStep++;
        ShowMiniGame(currentStep);
    }
}