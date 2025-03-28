using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    private GameManager _gameManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject finishLevelPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreMessage;
    

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnLevelFinish += ShowFinishLevelPanel;
    }

    private void ShowFinishLevelPanel(int finalScore)
    {
        scoreText.text = finalScore.ToString();
        finishLevelPanel.SetActive(true);
    }

    private void OnDisable()
    {
        _gameManager.OnLevelFinish -= ShowFinishLevelPanel;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
