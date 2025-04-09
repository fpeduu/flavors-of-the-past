using UnityEngine;
using UnityEngine.EventSystems;

public class DishDropReceiver : MonoBehaviour, IDropHandler
{
    // Esse método é chamado quando um objeto dragável é dropado sobre esse prato.
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            DraggableIngredient draggable = droppedObject.GetComponent<DraggableIngredient>();
            if (draggable != null)
            {
                Debug.Log("Ingrediente dropado no prato: " + draggable.ingredientType);

                // Torna o ingrediente filho do prato para que ele fique "sobre" o prato.
                droppedObject.transform.SetParent(transform);
                // Opcional: Você pode definir ou manter a posição solta ou usar um posicionamento especial.
                // Aqui, vamos deixar na posição que o jogador soltou.

                // Chama o OrderManager para incrementar a contagem.
                OrderManager.Instance.AddIngredient(draggable.ingredientType);
            }
            else
            {
                Debug.LogWarning("O objeto dropado não possui o script DraggableIngredient.");
            }
        }
    }
}
