using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    [Tooltip("Tempo limite para o minigame (em segundos)")]
    public float timeLimit = 60f;  // Exemplo: 60 segundos
    private float timeRemaining;

    [Header("UI do Timer")]
    [Tooltip("TextMeshProUGUI para exibir o tempo restante")]
    public TextMeshProUGUI timerText;

    [Header("UI de Fim de Jogo")]
    [Tooltip("Painel que fará o fade para preto no fim do jogo")]
    public GameObject endPanel;
    [Tooltip("Texto que exibe o resultado final e highscore")]
    public TextMeshProUGUI finalScoreText;
    [Tooltip("Duração do fade (em segundos)")]
    public float fadeDuration = 1f;
    [Tooltip("Tempo que o painel ficará exibindo o resultado final")]
    public float waitDuration = 4f;

    private bool gameEnded = false;

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerUI();

        // Configura o endPanel para iniciar com alpha 0
        if (endPanel != null)
        {
            endPanel.SetActive(true);
            Image panelImage = endPanel.GetComponent<Image>();
            Color col = panelImage.color;
            col.a = 0f;
            panelImage.color = col;
        }
    }

    void Update()
    {
        if (gameEnded) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
                timeRemaining = 0;
            UpdateTimerUI();
        }

        if (timeRemaining <= 0)
        {
            // Marca que o jogo acabou (para evitar múltiplas chamadas)
            gameEnded = true;

            // Chama a avaliação final do prato
            if (OrderManager.Instance != null)
            {
                OrderManager.Instance.EvaluateDish();
            }
            else
            {
                Debug.LogWarning("OrderManager não encontrado!");
            }

            // Inicia a sequência de fim de jogo
            StartCoroutine(EndSequence());
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Tempo: " + Mathf.CeilToInt(timeRemaining) + "s";
        else
            Debug.LogWarning("TimerText não está atribuído.");
    }

    IEnumerator EndSequence()
    {
        // Fade in do endPanel
        if (endPanel != null)
        {
            Image panelImage = endPanel.GetComponent<Image>();
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

        // Recupera o score final do OrderManager
        int finalScore = OrderManager.Instance != null ? OrderManager.Instance.GetTotalScore() : 0;
        // Recupera o highscore salvo (supondo que a chave seja "Highscore_Game", ajuste conforme necessário)
        int highscore = PlayerPrefs.GetInt("Highscore_Game", 0);

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + finalScore + "\nHighscore: " + highscore;
        }
        else
        {
            Debug.LogWarning("FinalScoreText não está atribuído.");
        }

        // Aguarda por waitDuration segundos
        yield return new WaitForSeconds(waitDuration);

        // Carrega a cena de seleção de minigames
        SceneManager.LoadScene("MinigameSelection");
    }
}
