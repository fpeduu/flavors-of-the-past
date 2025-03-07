using System.Collections;
using UnityEngine;

public class SaladIngredientSpawner : MonoBehaviour
{
    public GameObject[] ingredients;  // Prefabs (some good, some bad)
    public Transform spawnPoint;      // Where they appear
    public float spawnInterval = 2f;  // Seconds between spawns

    void Start()
    {
        StartCoroutine(SpawnIngredients());
    }

    IEnumerator SpawnIngredients()
    {
        while (true)
        {
            // Pick a random prefab
            int randomIndex = Random.Range(0, ingredients.Length);
            Instantiate(ingredients[randomIndex], spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
