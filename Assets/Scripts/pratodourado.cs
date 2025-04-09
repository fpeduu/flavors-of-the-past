using UnityEngine;

public class PratoDourado : MonoBehaviour
{
    public float velocidade = 3f;
    public float limiteEsquerda = -4f, limiteDireita = 4f;

    // Base points for the golden plate
    private int baseValue = 15;
    // Additional points for each consecutive hit
    private int consecutiveIncrement = 5;

    // Timer settings: if no hit occurs within resetTime seconds, the consecutive hits reset
    public float resetTime = 2f;
    private float timeSinceLastHit = 0f;

    public GameObject textoPontuacaoPrefab; // Prefab do texto flutuante
    public Canvas canvas; // Canvas principal da UI



    // Tracks consecutive hits (static so it persists across the game; remove static if per-instance is preferred)
    private static int consecutiveHits = 0;

    void Update()
    {
        // Move the plate horizontally
        transform.position += Vector3.right * velocidade * Time.deltaTime;

        if (transform.position.x > limiteDireita || transform.position.x < limiteEsquerda)
        {
            velocidade *= -1;
        }

        // Increase the timer
        timeSinceLastHit += Time.deltaTime;

        // If enough time has passed without a hit, reset the consecutive hit counter
        if (timeSinceLastHit >= resetTime)
        {
            ResetConsecutiveHits();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Macarrao"))
        {
            Destroy(other.gameObject);

            timeSinceLastHit = 0f;
            consecutiveHits++;

            int totalValue = baseValue + (consecutiveHits - 1) * consecutiveIncrement;
            Escorredor.instance.UpdateScore(totalValue);

            // Converte a posição do prato para posição em tela
            Vector3 posicaoTela = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);

            // Instancia o prefab no Canvas
            GameObject textoObj = Instantiate(textoPontuacaoPrefab, posicaoTela, Quaternion.identity, canvas.transform);

            // Configura o texto flutuante com o valor obtido
            PontuacaoFlutuante textoScript = textoObj.GetComponent<PontuacaoFlutuante>();
            textoScript.Configurar(totalValue);
        }
    }



    public static void ResetConsecutiveHits()
    {
        consecutiveHits = 0;
    }
}
