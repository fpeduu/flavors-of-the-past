using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public Transform plate;
    public float allowedOffset = 1.5f;
    
    public void CheckPlacement(Transform ingredient)
    {
        float offset = ingredient.position.x - plate.position.x;

        if (Mathf.Abs(offset) > allowedOffset)
        {
            Debug.Log("O ingrediente caiu errado!");
        }
        else
        {
            Debug.Log("O ingrediente caiu certo!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmos.DrawCube(plate.position, Vector3.one * allowedOffset);
    }
}
