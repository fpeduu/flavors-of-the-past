using UnityEngine;
using TMPro; // Caso esteja usando TextMeshPro; 
             // se estiver usando Text (UI), substitua para "using UnityEngine.UI;"

public class TextPulseAnimation : MonoBehaviour
{
    [Header("Configuração de Texto")]
    public TextMeshProUGUI targetText;
    // Se estiver usando Text (UI) comum, use: public Text targetText;

    [TextArea]
    public string mensagem = "Sirva os pratos!";
    // Frase que o texto mostrará

    [Header("Parâmetros de Animação")]
    public float minFontSize = 20f;  // Tamanho mínimo da fonte
    public float maxFontSize = 30f;  // Tamanho máximo da fonte
    public float pulseSpeed = 2f;    // Velocidade da oscilação (quanto maior, mais rápido a animação)

    private float currentTime = 0f;

    private void Start()
    {
        if (targetText != null)
        {
            // Exibe a mensagem desejada
            targetText.text = mensagem;
        }
        else
        {
            Debug.LogWarning("Nenhum TextMeshProUGUI atribuído ao script TextPulseAnimation!");
        }
    }

    private void Update()
    {
        if (targetText == null) return;

        // Atualiza o tempo
        currentTime += Time.deltaTime * pulseSpeed;

        // Calcula um fator de 0 a 1 usando um movimento senoidal
        // Oscila entre -1 e 1, então fazemos (Mathf.Sin(...) + 1) / 2 para obter [0..1].
        float sinFactor = (Mathf.Sin(currentTime) + 1f) / 2f;

        // Interpola entre minFontSize e maxFontSize com base no sinFactor
        float newSize = Mathf.Lerp(minFontSize, maxFontSize, sinFactor);

        // Aplica o tamanho ao texto
        targetText.fontSize = newSize;
    }
}
