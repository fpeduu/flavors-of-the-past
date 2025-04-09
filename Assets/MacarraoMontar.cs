using UnityEngine;

public class MacarraoMontar : MonoBehaviour
{
    // Offset base para posicionar os ingredientes sobre o prato
    public Vector3 baseOffset = new Vector3(-0.5f, 0.5f, 0f);
    // Intervalo para espalhar os ingredientes (pode ser ajustado conforme desejado)
    public Vector3 offsetIncrement = new Vector3(0.3f, -0.2f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dish"))
        {
            // Torna o macarrão filho do prato
            transform.SetParent(other.transform);

            // Calcula quantos ingredientes já estão no prato
            int currentCount = other.transform.childCount;
            // Define a posição do ingrediente com base no número de filhos
            // Por exemplo, a posição é baseOffset + (offsetIncrement * (currentCount - 1))
            // (Subtrai 1, pois este ingrediente ainda é considerado na contagem)
            Vector3 newLocalPosition = baseOffset + offsetIncrement * (currentCount - 1);
            transform.localPosition = newLocalPosition;

            // Notifica o OrderManager (ou OrderManager.Instance.AddIngredient("macarrao");)
            OrderManager.Instance.AddIngredient("macarrao");
        }
    }
}
