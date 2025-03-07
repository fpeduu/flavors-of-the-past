using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CarneController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform frigideira;
    public RectTransform prato;
    public GameObject carneAssada;
    public Text mensagem;

    private bool naFrigideira = false;
    private bool carnePronta = false;
    private Vector3 posicaoOriginal; // Guarda a posição inicial da carne

    private void Start()
    {
        posicaoOriginal = transform.position; // Armazena a posição original para resetar depois
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); // Mantém o objeto na frente
        transform.position = Input.mousePosition; // Move a carne com o mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(frigideira, Input.mousePosition))
        {
            if (!naFrigideira)
            {
                naFrigideira = true;
                mensagem.text = "Carne na frigideira! Aguarde...";
                StartCoroutine(AssarCarne());
            }
            // Retorna a carne para a posição inicial, pois sumiremos com ela depois
            // transform.position = posicaoOriginal;
        }
        else if (carnePronta && RectTransformUtility.RectangleContainsScreenPoint(prato, Input.mousePosition))
        {
            mensagem.text = "Hambúrguer pronto!";
            carneAssada.SetActive(false); // Esconde a carne assada
        }
        else
        {
            // Se não soltou em um local válido, volta para a posição original
            transform.position = posicaoOriginal;
        }
    }

    IEnumerator AssarCarne()
    {
        yield return new WaitForSeconds(5); // Simula tempo de assar
        carnePronta = true;
        mensagem.text = "Carne pronta! Arraste para o prato.";
        carneAssada.SetActive(true); // Exibe a carne assada
        gameObject.SetActive(false); // Some com a carne crua
    }
}
