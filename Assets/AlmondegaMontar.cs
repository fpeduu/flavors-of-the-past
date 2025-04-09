using UnityEngine;

public class AlmondegaMontar : MonoBehaviour
{
    public Vector3 baseOffset = new Vector3(0.5f, 0.5f, 0f);
    public Vector3 offsetIncrement = new Vector3(0.3f, -0.2f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dish"))
        {
            transform.SetParent(other.transform);
            int currentCount = other.transform.childCount;
            Vector3 newLocalPosition = baseOffset + offsetIncrement * (currentCount - 1);
            transform.localPosition = newLocalPosition;

            OrderManager.Instance.AddIngredient("almondega");
        }
    }
}
