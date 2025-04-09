using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableIngredient : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Tooltip("Tipo do ingrediente: ex. macarrao, almondega, molho")]
    public string ingredientType;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // Posição original caso o drop não seja válido
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Obtém o Canvas do objeto pai (garanta que o ingrediente esteja dentro de um Canvas)
        canvas = GetComponentInParent<Canvas>();

        // Salva a posição original para voltar caso não seja dropado sobre o prato
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Iniciou arrastar: " + ingredientType);
        canvasGroup.alpha = 0.6f;           // Reduz a opacidade para indicar arraste
        canvasGroup.blocksRaycasts = false;  // Permite que o prato detecte o drop
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atualiza a posição do ingrediente com base no delta do pointer
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Parou de arrastar: " + ingredientType);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        // Aqui, a lógica de verificação do drop fica no script do prato (IDropHandler)
        // Se o ingrediente não for dropado sobre o prato, o método OnDrop do prato não será chamado;
        // se você quiser retornar à posição original nesse caso, pode implementar aqui.
        // Por ora, vamos deixar que o prato receba o drop.
    }
}
