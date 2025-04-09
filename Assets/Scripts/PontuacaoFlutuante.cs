using TMPro;
using UnityEngine;

public class PontuacaoFlutuante : MonoBehaviour
{
    public TextMeshProUGUI texto;   // Referência para o componente de texto
    public float tempoDeVida = 1.5f;  // Duração total da animação (ajuste conforme necessário)
    public float velocidadeSubida = 20f;  // Velocidade de subida (em unidades por segundo)

    private float tempoDecorrido = 0f;   // Para controlar o tempo da animação
    private Color corOriginal;           // Cor original do texto, que usaremos para o fade

    void Start()
    {
        // Guarda a cor original para preservar os valores RGB
        corOriginal = texto.color;
    }

    void Update()
    {
        // Incrementa o tempo decorrido
        tempoDecorrido += Time.deltaTime;

        // Move o objeto para cima
        transform.Translate(Vector3.up * velocidadeSubida * Time.deltaTime);

        // Calcula o fator de interpolação (0 = início, 1 = fim)
        float fator = tempoDecorrido / tempoDeVida;

        // Faz o fade: interpolando o alfa de 1 até 0
        float novoAlpha = Mathf.Lerp(1f, 0f, fator);
        texto.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, novoAlpha);

        // Quando o tempo de vida terminar, destrói o objeto
        if (tempoDecorrido >= tempoDeVida)
        {
            Destroy(gameObject);
        }
    }

    // Método para configurar o texto com o valor da pontuação
    public void Configurar(int valor)
    {
        texto.text = $"+{valor}";
    }
}
