using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MinigameSelector : MonoBehaviour
{
    // Names of scenes for each minigame.
    public string[] minigameSceneNames;

    // Array of buttons for each minigame (for UI navigation).
    public Button[] buttons;

    // Array of TextMeshProUGUI elements that will display each minigame’s highscore.
    // This allows you to position each highscore individually.
    public TextMeshProUGUI[] highscoreTexts;

    // Array of unique keys (one per minigame) for storing highscore via PlayerPrefs.
    // Example: "Highscore_Pasta", "Highscore_Minigame2", etc.
    public string[] highscoreKeys;

    private void Start()
    {
        // Select the first button by default.
        if (buttons != null && buttons.Length > 0)
        {
            buttons[0].Select();
        }

        // For now, update only the first highscore (index 0) from the first minigame.
        if (highscoreKeys != null && highscoreKeys.Length > 0 &&
            highscoreTexts != null && highscoreTexts.Length > 0)
        {
            int highscore = PlayerPrefs.GetInt(highscoreKeys[0], 0);
            highscoreTexts[0].text = "Highscore: " + highscore;
        }
    }

    // Called when a minigame button is pressed.
    public void StartMinigame(int minigameIndex)
    {
        Debug.Log("Starting minigame index: " + minigameIndex);
        if (minigameIndex >= 0 && minigameIndex < minigameSceneNames.Length)
        {
            SceneManager.LoadScene(minigameSceneNames[minigameIndex]);
        }
        else
        {
            Debug.LogWarning("Minigame index out of range!");
        }
    }

    // Load the recipe selection scene.
    public void LoadSelectRecipeScene()
    {
        SceneManager.LoadScene("RecipeSelection");
    }
}
