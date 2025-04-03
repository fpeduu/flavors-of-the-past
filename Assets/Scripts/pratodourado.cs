using UnityEngine;

public class PratoDourado : MonoBehaviour
{
    public float velocidade = 3f;
    public float limiteEsquerda = -4f, limiteDireita = 4f;

    // Base points for the golden plate
    private int baseValue = 15;
    // Additional points for each consecutive hit
    private int consecutiveIncrement = 5;

    // Timer settings: if no hit occurs within resetTime seconds, the consecutive hits reset
    public float resetTime = 2f;
    private float timeSinceLastHit = 0f;

    // Tracks consecutive hits (static so it persists across the game; remove static if per-instance is preferred)
    private static int consecutiveHits = 0;

    void Update()
    {
        // Move the plate horizontally
        transform.position += Vector3.right * velocidade * Time.deltaTime;

        if (transform.position.x > limiteDireita || transform.position.x < limiteEsquerda)
        {
            velocidade *= -1;
        }

        // Increase the timer
        timeSinceLastHit += Time.deltaTime;

        // If enough time has passed without a hit, reset the consecutive hit counter
        if (timeSinceLastHit >= resetTime)
        {
            ResetConsecutiveHits();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Macarrao"))
        {
            // The pasta has been caught
            Destroy(other.gameObject);

            // Reset the timer on a successful hit
            timeSinceLastHit = 0f;

            // Increase consecutive hits count
            consecutiveHits++;

            // Calculate the total points: base value + additional bonus for consecutive hits
            int totalValue = baseValue + (consecutiveHits - 1) * consecutiveIncrement;
            Escorredor.instance.UpdateScore(totalValue);
        }
    }

    public static void ResetConsecutiveHits()
    {
        consecutiveHits = 0;
    }
}
