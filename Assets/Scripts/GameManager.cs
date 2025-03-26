using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }
    #endregion


    public bool isGameOver = false;
    
    #region Score Management
    [Header("Score management")]
    [Tooltip("Number of notes needed to increase multiply value")]
    [SerializeField] private int streakSequence = 7;
    [Tooltip("Max multiply value")]
    [SerializeField] private int maxMultiply = 4;
    
    public event Action<int> OnLevelFinish;
    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private Slider hitStreakSlider;

    private int _score;
    private int _hitStreak;
    private int _multiplyValue;

    public void IncrementScore(int score)
    {
        _score += score * _multiplyValue;
        scoreText.text = "Score: " + _score;
        _hitStreak++;

        if (_hitStreak >= streakSequence && _multiplyValue < maxMultiply)
        {
            IncrementMultiplier();
            _hitStreak = 0;
        }
        hitStreakSlider.value = _hitStreak;
    }

    public void ResetStreak()
    {
        _hitStreak = 0;
        hitStreakSlider.value = _hitStreak;
        _multiplyValue = 1;
        multiplierText.text = "x" + _multiplyValue;
    }

    void IncrementMultiplier()
    {
        _multiplyValue++;
        multiplierText.text = "x" + _multiplyValue;
    }
    private void ResetScore()
    {
        _score = 0;
        scoreText.text = "Score: " + _score;
        ResetStreak();
    }

    #endregion

    public void FinishLevel()
    {
        isGameOver = true;
        OnLevelFinish?.Invoke(_score);
    }

    private void Start()
    {
        ResetScore();
        hitStreakSlider.maxValue = streakSequence;
    }
}