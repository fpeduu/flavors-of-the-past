using UnityEngine;

public class BellController : MonoBehaviour
{
    // Esse método será chamado quando o usuário clicar na campainha.
    // Você pode associá-lo ao botão (OnClick) na sua interface.
    public void OnBellPressed()
    {
        Debug.Log("Campainha pressionada!");

        // Verifica se o OrderManager existe e chama o método de avaliação do prato.
        if (OrderManager.Instance != null)
        {
            OrderManager.Instance.EvaluateDish();
            Debug.Log("Avaliação do prato iniciada.");
        }
        else
        {
            Debug.LogWarning("OrderManager não encontrado na cena!");
        }
    }
}
