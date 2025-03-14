using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TomatoSpawner : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public float spawnRate = 1.5f;
    public float minX = -5f, maxX = 5f;
    public float throwForce = 7f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnTomato), 3f, spawnRate);
    }

    void SpawnTomato()
    {
        if (!TomatoGameManager.Instance.isGameActive) return;
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), -4f, 0);
        GameObject tomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = tomato.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(0, throwForce);
    }
}
