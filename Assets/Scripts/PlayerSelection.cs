using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public ScoreManager scoreManager; // Reference to ScoreManager

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            // Convert mouse position to world position
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Cast a 2D ray from that point
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if the clicked object has an Ingredient script
                Ingredient ingredient = hit.collider.GetComponent<Ingredient>();
                if (ingredient != null)
                {
                    // Good ingredient => +1 point
                    // Bad ingredient => -1 point
                    if (ingredient.isGoodIngredient)
                    {
                        scoreManager.AddScore(1);
                        Debug.Log("Clicked a GOOD ingredient! +1");
                    }
                    else
                    {
                        scoreManager.AddScore(-1);
                        Debug.Log("Clicked a BAD ingredient! -1");
                    }

                    // Destroy the ingredient after scoring
                    Destroy(ingredient.gameObject);
                }
            }
        }
    }
}
