using System.Collections;
using UnityEngine;
using TMPro;

public class Panela : MonoBehaviour
{
    public GameObject macarraoPrefab;
    public Transform spawnPoint;

    public float intervaloSpawn = 1.5f;
    public float frenesiInterval = 0.3f;
    public float frenesiDuration = 2f;

    // Reference to your Canvas (for other UI operations if needed)
    public Canvas canvas;

    // The TextMeshProUGUI in the scene for Frenesi countdown
    public TextMeshProUGUI frenesiCountdownText;

    // A small offset in world space for the Frenesi text so it appears near the Panela.
    // This offset is added before converting to screen space.
    public Vector3 frenesiTextOffset = new Vector3(1f, 0f, 0f);

    // Control flags
    private bool isFrenesiActive = false;
    private bool isCountdownActive = false;

    void Start()
    {
        // Regular macarrão spawn.
        InvokeRepeating("GerarMacarrao", 0f, intervaloSpawn);
        // Check chance for Frenesi every second.
        InvokeRepeating("ChecarFrenesi", 1f, 1f);
    }

    void GerarMacarrao()
    {
        Instantiate(macarraoPrefab, spawnPoint.position, Quaternion.identity);
    }

    void ChecarFrenesi()
    {
        // Only check if not currently in Frenesi mode or during the countdown.
        if (!isFrenesiActive && !isCountdownActive)
        {
            // 1 in 10 chance to start Frenesi.
            if (Random.Range(0, 10) == 0)
            {
                StartCoroutine(ContagemFrenesi());
            }
        }
    }

    IEnumerator ContagemFrenesi()
    {
        isCountdownActive = true;

        // Countdown from 3 to 0.
        for (int count = 3; count >= 0; count--)
        {
            // Calculate the world position based on Panela position and offset.
            Vector3 worldPos = transform.position + frenesiTextOffset;
            // Convert world position to screen space.
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            // Move the text 100 pixels to the left.
            screenPos.x -= 150f;

            // Update the text position.
            frenesiCountdownText.transform.position = screenPos;
            // Update the text content.
            frenesiCountdownText.text = "Cuidado, modo Frenesi em " + count;

            yield return new WaitForSeconds(1f);
        }

        // Clear the text when countdown finishes.
        frenesiCountdownText.text = "";

        AtivarFrenesi();
        isCountdownActive = false;
    }

    void AtivarFrenesi()
    {
        // Stop the regular macarrão spawn.
        CancelInvoke("GerarMacarrao");
        isFrenesiActive = true;
        // Start rapid spawn for Frenesi.
        InvokeRepeating("GerarMacarrao", 0f, frenesiInterval);
        // End Frenesi after a set duration.
        StartCoroutine(FrenesiDuracao());
    }

    IEnumerator FrenesiDuracao()
    {
        yield return new WaitForSeconds(frenesiDuration);
        DesativarFrenesi();
    }

    void DesativarFrenesi()
    {
        // Stop the Frenesi spawn and revert to normal spawn.
        CancelInvoke("GerarMacarrao");
        isFrenesiActive = false;
        InvokeRepeating("GerarMacarrao", 0f, intervaloSpawn);
    }
}
