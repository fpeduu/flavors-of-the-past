using UnityEngine;
using TMPro;

public class TomatoGameManager : MonoBehaviour
{
    public static TomatoGameManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Game Settings")]
    public float totalGameTime = 33f;
    public float spawnStartDelay = 3f;

    private int score = 0;
    private float timeRemaining;
    public bool isGameActive = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = totalGameTime;
        UpdateScore(0);
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isGameActive) return;

        timeRemaining -= Time.deltaTime;

        UpdateTimerDisplay();
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isGameActive = false;
            EndGame();
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        if (isGameActive)
        {
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
        else
        {
            timerText.text = "TIME'S UP!";
        }
    }

    public bool ShouldSpawnTomatoes()
    {
        return isGameActive && timeRemaining <= (totalGameTime - spawnStartDelay);
    }

    void EndGame()
    {
        // Add any end-game logic here
        Debug.Log("Game Over!");
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
}