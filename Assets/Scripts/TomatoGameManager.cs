using UnityEngine;
using TMPro;

public class TomatoGameManager : MonoBehaviour
{
    public static TomatoGameManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI comboText;

    [Header("Game Settings")]
    public float totalGameTime = 33f;
    public float spawnStartDelay = 3f;

    [Header("Combo Settings")]
    [SerializeField] private Color[] comboColors;
    [SerializeField] private float comboDisplayTime = 1.5f;

    private float timeRemaining;
    private bool isGameActive = true;
    private float comboDisplayTimer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = totalGameTime;
        ScoreManager.Instance.OnScoreChanged.AddListener(UpdateScoreDisplay);
        ScoreManager.Instance.OnComboChanged.AddListener(UpdateComboDisplay);
        UpdateScoreDisplay(ScoreManager.Instance.GetCurrentScore());
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isGameActive) return;

        timeRemaining -= Time.deltaTime;
        comboDisplayTimer += Time.deltaTime;

        UpdateTimerDisplay();

        if (comboDisplayTimer >= comboDisplayTime)
        {
            comboText.text = "";
        }

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isGameActive = false;
            EndGame();
            UpdateTimerDisplay();
        }
    }

    void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }

    void UpdateComboDisplay(int combo)
    {
        if (combo > 1)
        {
            comboDisplayTimer = 0f;
            comboText.text = $"{combo}x COMBO!";
            comboText.color = comboColors[Mathf.Clamp(combo - 1, 0, comboColors.Length - 1)];
        }
        else
        {
            comboText.text = "";
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = isGameActive
            ? Mathf.CeilToInt(timeRemaining).ToString()
            : "TIME'S UP!";
    }

    public bool ShouldSpawnTomatoes()
    {
        return isGameActive && timeRemaining <= (totalGameTime - spawnStartDelay);
    }

    void EndGame()
    {
        // Add end-game logic here
        Debug.Log($"Final Score: {ScoreManager.Instance.GetCurrentScore()}");
    }

    public bool IsGameActive() => isGameActive;
}