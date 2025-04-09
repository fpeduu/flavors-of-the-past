using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Escorredor : MonoBehaviour
{
    public static Escorredor instance;

    // Movement and drop point.
    public float velocidade = 5f;
    public Transform pontoDeSoltar;

    // Macarrão carrying limit.
    private int macarraoCount = 0;
    private int maxMacarrao = 5;

    // Score and UI element to show score and timer.
    private int score = 0;
    public TextMeshProUGUI scoreText;

    // "Cheia!" text when at full capacity.
    public TextMeshProUGUI cheiaText;
    public Vector3 cheiaTextScreenOffset = new Vector3(150f, 0f, 0f);
    public float oscillationAmplitude = 5f;
    public float oscillationFrequency = 0.5f;

    // Game duration and timer.
    public float gameDuration = 25f;
    private float gameTimeRemaining;
    private bool gameActive = true;

    // Highscore (unique for this minigame) stored in PlayerPrefs.
    public int highscore;
    public string highscoreKey = "Highscore_Pasta";

    // End-of-game UI: a panel that fades to black plus a text to show final score.
    public GameObject endPanel;
    public TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Initialize timer.
        gameTimeRemaining = gameDuration;
        // Retrieve the highscore saved using the unique key.
        highscore = PlayerPrefs.GetInt(highscoreKey, 0);
        UpdateUI();

        // Clear the "cheia!" text.
        if (cheiaText != null)
            cheiaText.text = "";

        // Set up the end panel: make sure it is active but fully transparent.
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
    }

    void Update()
    {
        if (!gameActive)
            return;

        // Horizontal movement.
        float movimento = Input.GetAxis("Horizontal") * velocidade * Time.deltaTime;
        transform.position += new Vector3(movimento, 0, 0);

        // Update timer.
        gameTimeRemaining -= Time.deltaTime;
        if (gameTimeRemaining <= 0f)
        {
            gameTimeRemaining = 0f;
            EndGame();
        }
        UpdateUI();

        // Update "cheia!" text position with offset and vertical oscillation.
        if (macarraoCount == maxMacarrao && cheiaText != null)
        {
            Vector3 worldPos = transform.position + new Vector3(2f, 0f, 0f);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            screenPos += cheiaTextScreenOffset;
            float oscillationOffsetY = Mathf.Sin(Time.time * oscillationFrequency * 2 * Mathf.PI) * oscillationAmplitude;
            screenPos += new Vector3(0f, oscillationOffsetY, 0f);
            cheiaText.transform.position = screenPos;
        }

        // If SPACE is pressed and at least one macarrão is carried, release one.
        if (Input.GetKeyDown(KeyCode.Space) && macarraoCount > 0)
        {
            SoltarMacarrao();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameActive)
            return;

        if (other.CompareTag("Macarrao"))
        {
            // If already full, let the macarrão pass.
            if (macarraoCount >= maxMacarrao)
                return;

            // Capture the macarrão.
            macarraoCount++;
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
            otherRb.bodyType = RigidbodyType2D.Kinematic;
            otherRb.linearVelocity = Vector2.zero;

            // When capacity is reached, display "cheia!".
            if (macarraoCount == maxMacarrao && cheiaText != null)
                cheiaText.text = "cheia!";
        }
    }

    public void SoltarMacarrao()
    {
        if (!gameActive)
            return;

        if (macarraoCount > 0)
        {
            // Remove one macarrão (removing the last attached child).
            int childIndex = transform.childCount - 1;
            Transform macarrao = transform.GetChild(childIndex);
            macarrao.SetParent(null);
            macarrao.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            macarrao.position = pontoDeSoltar.position;
            macarraoCount--;

            // Clear "cheia!" text if no longer full.
            if (macarraoCount < maxMacarrao && cheiaText != null)
                cheiaText.text = "";
        }
    }

    public void UpdateScore(int value)
    {
        if (!gameActive)
            return;

        score += value;
        UpdateUI();
    }

    // Updates the scoreText to show the current score and the remaining time.
    private void UpdateUI()
    {
        scoreText.text = "Pontuação: " + score + " | Tempo: " + Mathf.CeilToInt(gameTimeRemaining);
    }

    // Called when the game timer reaches 0.
    private void EndGame()
    {
        gameActive = false;

        // If the current score is higher than the saved highscore, update and save it.
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt(highscoreKey, highscore);
            PlayerPrefs.Save();
        }

        // Begin the end-of-game sequence: fade to black, show final score and highscore, then load scene.
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
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

        if (finalScoreText != null)
        {
            // Display both the final score and the highscore on the panel.
            finalScoreText.text = "Final Score: " + score + "\nHighscore: " + highscore;
        }

        // Wait for 4 seconds before transitioning.
        yield return new WaitForSeconds(4f);

        // Load the "MinigameSelection" scene.
        SceneManager.LoadScene("MinigameSelection");
    }
}
