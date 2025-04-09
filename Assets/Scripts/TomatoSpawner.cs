using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TomatoSpawner : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public float baseSpawnRate = 1.5f;
    public float minSpawnRate = 0.4f;
    public float minX = -5f, maxX = 5f;
    public float baseThrowForce = 7f;
    public float difficultyRampTime = 25f;

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (!TomatoGameManager.Instance.ShouldSpawnTomatoes()) return;

        float timeElapsed = TomatoGameManager.Instance.totalGameTime - TomatoGameManager.Instance.GetTimeRemaining();
        float difficultyPercent = Mathf.Clamp01(timeElapsed / difficultyRampTime);

        float currentSpawnRate = Mathf.Lerp(baseSpawnRate, minSpawnRate, difficultyPercent);

        if (Time.time >= nextSpawnTime)
        {
            SpawnTomato(difficultyPercent);
            nextSpawnTime = Time.time + currentSpawnRate;
        }
    }

    void SpawnTomato(float difficulty)
    {
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), -4f, 0);
        GameObject tomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = tomato.GetComponent<Rigidbody2D>();
        float throwForce = baseThrowForce + difficulty * 2f; // More upward force as difficulty increases
        float lateralJitter = Random.Range(-3f, 3f) * difficulty;

        rb.linearVelocity = new Vector2(lateralJitter, throwForce);

        // Pass difficulty to tomato for more fine-grained behavior
        Tomato tomatoScript = tomato.GetComponent<Tomato>();
        if (tomatoScript != null)
        {
            tomatoScript.SetDifficultyMultiplier(difficulty);
        }
    }
}
