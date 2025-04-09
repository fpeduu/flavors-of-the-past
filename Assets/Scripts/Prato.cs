using UnityEngine;

public class Prato : MonoBehaviour
{
    public float velocidade = 3f;
    public float limiteEsquerda = -4f, limiteDireita = 4f;

    // Valor de pontos para os pratos normais
    private int value = 10;

    // Referências para exibição do texto flutuante (configure via Inspector)
    public GameObject textoPontuacaoPrefab; // Prefab do texto flutuante com animação
    public Canvas canvas;                   // Canvas principal onde o texto será exibido

    void Update()
    {
        // Movimenta o prato horizontalmente
        transform.position += Vector3.right * velocidade * Time.deltaTime;

        // Inverte a direção quando atinge os limites
        if (transform.position.x > limiteDireita || transform.position.x < limiteEsquerda)
        {
            velocidade *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Macarrao"))
        {
            // Remove o macarrão coletado
            Destroy(other.gameObject);

            // Atualiza a pontuação geral
            Escorredor.instance.UpdateScore(value);

            // Converte a posição do prato para posição na tela (ajuste o deslocamento se necessário)
            Vector3 posicaoTela = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);

            // Instancia o prefab do texto pontuação no Canvas
            GameObject textoObj = Instantiate(textoPontuacaoPrefab, posicaoTela, Quaternion.identity, canvas.transform);

            // Configura o texto do prefab com o valor de pontos
            PontuacaoFlutuante textoScript = textoObj.GetComponent<PontuacaoFlutuante>();
            textoScript.Configurar(value);
        }
    }
}
