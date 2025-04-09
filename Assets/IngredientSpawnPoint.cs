using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientSpawnPoint : MonoBehaviour, IPointerDownHandler
{
    [Tooltip("Prefab do ingrediente que será instanciado ao clicar neste ponto")]
    public GameObject ingredientPrefab;

    [Tooltip("Tipo do ingrediente (ex.: macarrao, almondega, molho)")]
    public string ingredientType;

    // Você pode definir um intervalo para evitar cliques repetidos,
    // mas para o exemplo, vamos instanciar sempre que clicar.

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("SpawnPoint clicado para: " + ingredientType);

        if (ingredientPrefab != null)
        {
            // Instancia o ingrediente na posição deste ponto de spawn
            // Se o ponto de spawn for um objeto UI, ele já possui um RectTransform e está no Canvas.
            GameObject newIngredient = Instantiate(ingredientPrefab, transform.position, Quaternion.identity, transform.parent);
            Debug.Log("Ingrediente instanciado: " + ingredientType);

            // Se desejar, você pode alterar a posição do novo ingrediente para,
            // por exemplo, ficar ligeiramente offset do ponto de spawn (opcional)
            // newIngredient.GetComponent<RectTransform>().anchoredPosition += new Vector2(10f, 0f);

            // Como o prefab já possui o script DraggableIngredient, o jogador pode imediatamente arrastar o novo objeto.
            // (O sistema de eventos do UI já fará a detecção de pointer e o drag & drop funcionará normalmente.)
        }
        else
        {
            Debug.LogWarning("Nenhum prefab de ingrediente atribuído ao spawn point para: " + ingredientType);
        }
    }
}
