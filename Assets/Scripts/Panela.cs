using UnityEngine;

public class Panela : MonoBehaviour
{
    public GameObject macarraoPrefab;
    public Transform spawnPoint;
    public float intervaloSpawn = 1.5f;

    void Start()
    {
        InvokeRepeating("GerarMacarrao", 0f, intervaloSpawn);
    }

    void GerarMacarrao()
    {
        Instantiate(macarraoPrefab, spawnPoint.position, Quaternion.identity);
    }
}