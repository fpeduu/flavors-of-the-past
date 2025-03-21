using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // If using TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI pontuacaoText; // Reference to a TextMeshPro UI element
    private int pontuacao = 0;            // Current score

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        pontuacao += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (pontuacaoText == null) return;
        
        pontuacaoText.text = "Pontuação: " + pontuacao;
    }

}
