using UnityEngine;

public class IngredientSpawn : MonoBehaviour
{
    [Header("Prefabs dos Ingredientes")]
    public GameObject macarraoPrefab;
    public GameObject almondegaPrefab;
    public GameObject molhoPrefab;

    [Header("Pontos de Spawn")]
    public Transform macarraoSpawnPoint;
    public Transform almondegaSpawnPoint;
    public Transform molhoSpawnPoint;

    [Header("Intervalo de Spawn (segundos)")]
    public float spawnInterval = 5f;

    private void Start()
    {
        // Podemos usar InvokeRepeating para spawnear os ingredientes
        InvokeRepeating("SpawnMacarrao", 1f, spawnInterval);
        InvokeRepeating("SpawnAlmondega", 1.2f, spawnInterval);
        InvokeRepeating("SpawnMolho", 1.4f, spawnInterval);
    }

    void SpawnMacarrao()
    {
        if (macarraoPrefab != null && macarraoSpawnPoint != null)
        {
            // Instancia o prefab no local do ponto de spawn
            GameObject ingredient = Instantiate(macarraoPrefab, macarraoSpawnPoint.position, macarraoSpawnPoint.rotation, macarraoSpawnPoint.parent);
            Debug.Log("Spawning Macarrão");
        }
    }

    void SpawnAlmondega()
    {
        if (almondegaPrefab != null && almondegaSpawnPoint != null)
        {
            GameObject ingredient = Instantiate(almondegaPrefab, almondegaSpawnPoint.position, almondegaSpawnPoint.rotation, almondegaSpawnPoint.parent);
            Debug.Log("Spawning Almôndega");
        }
    }

    void SpawnMolho()
    {
        if (molhoPrefab != null && molhoSpawnPoint != null)
        {
            GameObject ingredient = Instantiate(molhoPrefab, molhoSpawnPoint.position, molhoSpawnPoint.rotation, molhoSpawnPoint.parent);
            Debug.Log("Spawning Molho");
        }
    }
}
