using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // If using TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI pontuacaoText; // Reference to a TextMeshPro UI element
    private int pontuacao = 0;            // Current score
    private float gameDuration = 15f;     // Duration before scene change

    void Start()
    {
        UpdateScoreUI();
        Invoke("ChangeScene", gameDuration);
    }

    public void AddScore(int value)
    {
        pontuacao += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (pontuacaoText != null)
        {
            pontuacaoText.text = "Pontuação: " + pontuacao;
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("MontarMinigame");
    }
}
