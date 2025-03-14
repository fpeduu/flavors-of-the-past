using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TomatoGameManager : MonoBehaviour
{
    public static TomatoGameManager Instance;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
