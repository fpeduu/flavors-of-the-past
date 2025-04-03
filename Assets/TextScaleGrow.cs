using UnityEngine;
using TMPro;

public class TextScaleGrow : MonoBehaviour
{
    [Header("Configurações de Oscilação do Texto")]
    public TextMeshProUGUI textMeshPro;  // Referência ao componente TextMeshProUGUI
    public float baseFontSize = 28f;     // Tamanho central da fonte
    public float amplitude = 1f;         // Amplitude da oscilação (28 ± 1 resulta em 27 a 29)
    public float speed = 1f;             // Velocidade da oscilação (ciclos por segundo)

    void Update()
    {
        // Calcula o novo tamanho usando uma função seno para criar a oscilação suave
        float novoTamanho = baseFontSize + amplitude * Mathf.Sin(Time.time * speed * 2 * Mathf.PI);
        textMeshPro.fontSize = novoTamanho;
    }
}
