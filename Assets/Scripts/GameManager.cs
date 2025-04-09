using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public bool isGameOver = false;

    #region Score Management
    [Header("Score Management")]
    [SerializeField] private int streakSequence = 7;     // notas p/ aumentar multiplicador
    [SerializeField] private int maxMultiply = 4;        // multiplicador máximo

    public event Action<int> OnLevelFinish;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private Slider hitStreakSlider;

    private int _score;
    private int _hitStreak;
    private int _multiplyValue;
    #endregion

    // Chave para highscore
    public string highscoreKey = "Highscore_Game2";
    public int highscore;

    // Fim de jogo (UI e painel de transição)
    public GameObject endPanel;
    public TextMeshProUGUI finalScoreText;

    private void Start()
    {
        ResetScore();
        hitStreakSlider.maxValue = streakSequence;

        // Carrega highscore
        highscore = PlayerPrefs.GetInt(highscoreKey, 0);

        // Garante que o endPanel comece invisível
        if (endPanel != null)
        {
            endPanel.SetActive(true);
            Image panelImage = endPanel.GetComponent<Image>();
            Color col = panelImage.color;
            col.a = 0f;
            panelImage.color = col;
        }
        if (finalScoreText != null)
            finalScoreText.text = "";

        // Atualiza a UI inicial
        UpdateUI();
    }

    // Chamado ao obter pontuação
    public void IncrementScore(int score)
    {
        _score += score * _multiplyValue;

        _hitStreak++;
        if (_hitStreak >= streakSequence && _multiplyValue < maxMultiply)
        {
            IncrementMultiplier();
            _hitStreak = 0;
        }

        UpdateUI();
    }

    public void ResetStreak()
    {
        _hitStreak = 0;
        _multiplyValue = 1;
        hitStreakSlider.value = _hitStreak;
        multiplierText.text = "x" + _multiplyValue;
    }

    private void ResetScore()
    {
        _score = 0;
        ResetStreak();
        // Aqui você pode chamar UpdateUI() também, se quiser mostrar "Score: 0"
        // mas normalmente chamamos depois que a cena já estiver preparada
    }

    void IncrementMultiplier()
    {
        _multiplyValue++;
        multiplierText.text = "x" + _multiplyValue;
    }

    // Atualiza os elementos de UI (Score, Multiplicador, Slider)
    private void UpdateUI()
    {
        // Score
        if (scoreText != null)
            scoreText.text = "Score: " + _score;

        // Multiplicador
        if (multiplierText != null)
            multiplierText.text = "x" + _multiplyValue;

        // HitStreak slider
        if (hitStreakSlider != null)
            hitStreakSlider.value = _hitStreak;
    }

    public void FinishLevel()
    {
        isGameOver = true;
        OnLevelFinish?.Invoke(_score);
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        // Atualiza e salva highscore, se houver
        if (_score > highscore)
        {
            highscore = _score;
            PlayerPrefs.SetInt(highscoreKey, highscore);
            PlayerPrefs.Save();
        }

        // Animação de fade para o endPanel
        if (endPanel != null)
        {
            Image panelImage = endPanel.GetComponent<Image>();
            float fadeDuration = 1f;
            float elapsed = 0f;
            Color col = panelImage.color;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsed / fadeDuration);
                col.a = alpha;
                panelImage.color = col;
                yield return null;
            }
            col.a = 1f;
            panelImage.color = col;
        }

        // Exibe score e highscore finais
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + _score + "\nHighscore: " + highscore;
        }

        // Espera alguns segundos
        yield return new WaitForSeconds(4f);

        // Carrega cena de seleção de minigames
        SceneManager.LoadScene("MinigameSelection");
    }
}
