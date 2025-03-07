using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool isGoodIngredient; // True for good, false for bad
    public float speed = 2f;      // Movement speed

    void Update()
    {
        // Move to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Destroy if it goes too far left
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
