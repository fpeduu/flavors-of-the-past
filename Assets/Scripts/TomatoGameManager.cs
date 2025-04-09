using UnityEngine;
using UnityEngine.Events;
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
    public bool isGameActive = true;
    private float comboDisplayTimer;

    public int MaxComboMultiplier = 5;
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    public UnityEvent<int> OnComboChanged = new UnityEvent<int>();

    private int currentScore;
    private int currentCombo;
    private float lastCutTime;
    private float comboTimeout = 2f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = totalGameTime;
        UpdateScoreDisplay(TomatoGameManager.Instance.GetCurrentScore());
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isGameActive) return;

        timeRemaining -= Time.deltaTime;
        comboDisplayTimer += Time.deltaTime;

        UpdateScoreDisplay(currentScore);
        UpdateTimerDisplay();

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isGameActive = false;
            EndGame();
            UpdateTimerDisplay();
        }

        if (Time.time - lastCutTime > comboTimeout && currentCombo > 0)
        {
            ResetCombo();
        }
    }

    public void AddScore(int points)
    {
        int multiplier = Mathf.FloorToInt(currentCombo / 2f);
        currentScore += points * Mathf.Max(1, multiplier); // Ensure at least x1 multiplier
        Debug.Log($"Current Score: {currentScore}");

        currentCombo++;
        Debug.Log($"Current Combo: {currentCombo}");
        OnComboChanged.Invoke(currentCombo);
        lastCutTime = Time.time;
    }

    public void RemoveScore(int points)
    {
        currentScore -= points;
        if (currentScore < 0) currentScore = 0;
        OnScoreChanged.Invoke(currentScore);
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        OnComboChanged.Invoke(currentCombo);
    }

    public int GetCurrentScore() => currentScore;
    public int GetCurrentCombo() => currentCombo;

    void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
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
        Debug.Log($"Final Score: {TomatoGameManager.Instance.GetCurrentScore()}");
    }

    public bool IsGameActive() => isGameActive;
}