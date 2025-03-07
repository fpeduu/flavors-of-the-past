using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarneAssadaController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform prato;
    public Text mensagem;
    private Vector3 posicaoOriginal;

    private void Start()
    {
        posicaoOriginal = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(prato, Input.mousePosition))
        {
            // mensagem.text = "Hambúrguer pronto!";
            Debug.Log("Hambúrguer pronto!");
            // gameObject.SetActive(false); // Esconde a carne assada (já no prato)
        }
        else
        {
            transform.position = posicaoOriginal; // Retorna para a posição inicial
        }
    }
}
