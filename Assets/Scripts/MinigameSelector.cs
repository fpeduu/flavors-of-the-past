using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameSelector : MonoBehaviour
{
    public string[] minigameSceneNames;
    public Button[] buttons;

    private void Start()
    {
        buttons[0].Select();
    }

    public void StartMinigame(int minigameIndex)
    {
        Debug.Log(minigameIndex);
        SceneManager.LoadScene(minigameSceneNames[minigameIndex]);
    }

    public void LoadSelectRecipeScene()
    {
        SceneManager.LoadScene("RecipeSelection");
    }
    
}
