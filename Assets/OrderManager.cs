using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    // Singleton para acesso global
    public static OrderManager Instance { get; private set; }

    [Header("Pedido Atual")]
    [Tooltip("Quantidade requerida de macarrão")]
    public int requiredMacarrao;
    [Tooltip("Quantidade requerida de almôndegas")]
    public int requiredAlmondegas;
    [Tooltip("Quantidade requerida de molho")]
    public int requiredMolho;

    [Header("Ingredientes no Prato")]
    public int currentMacarrao;
    public int currentAlmondegas;
    public int currentMolho;

    [Header("UI")]
    [Tooltip("Texto para exibir o pedido atual")]
    public TextMeshProUGUI orderText;
    [Tooltip("Texto para exibir os ingredientes atualmente no prato")]
    public TextMeshProUGUI dishStatusText;
    [Tooltip("Texto para exibir a pontuação total")]
    public TextMeshProUGUI totalScoreText;

    [Header("Pontuação")]
    [Tooltip("Pontuação máxima para o prato perfeito")]
    public int perfectScore = 50;
    [Tooltip("Pontos deduzidos por cada unidade faltante ou extra")]
    public int penaltyPerUnit = 10;
    private int totalScore = 0;

    private void Awake()
    {
        // Implementação do Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Define o pedido inicial (exemplo: 4 macarrões, 5 almôndegas, 3 molho)
        requiredMacarrao = 4;
        requiredAlmondegas = 5;
        requiredMolho = 3;

        ResetDish();
        UpdateUI();
    }

    // Este método é chamado quando um ingrediente é adicionado (via drop)
    public void AddIngredient(string ingredientType)
    {
        switch (ingredientType.ToLower())
        {
            case "macarrao":
                currentMacarrao++;
                Debug.Log("Macarrão adicionado. Total atual: " + currentMacarrao);
                break;
            case "almondega":
                currentAlmondegas++;
                Debug.Log("Almôndega adicionada. Total atual: " + currentAlmondegas);
                break;
            case "molho":
                currentMolho++;
                Debug.Log("Molho adicionado. Total atual: " + currentMolho);
                break;
            default:
                Debug.LogWarning("Ingrediente desconhecido: " + ingredientType);
                break;
        }

        UpdateDishStatusUI();
    }

    // Método para avaliar o prato (por exemplo, quando o jogador aperta a campainha)
    // Você pode chamar esse método via um botão na UI.
    public void EvaluateDish()
    {
        // Calcula a diferença entre o que foi colocado e o que foi pedido
        int diffMacarrao = Mathf.Abs(currentMacarrao - requiredMacarrao);
        int diffAlmondegas = Mathf.Abs(currentAlmondegas - requiredAlmondegas);
        int diffMolho = Mathf.Abs(currentMolho - requiredMolho);

        int totalPenalty = penaltyPerUnit * (diffMacarrao + diffAlmondegas + diffMolho);
        int dishScore = Mathf.Max(perfectScore - totalPenalty, 0);

        totalScore += dishScore;
        Debug.Log("Prato avaliado. Pontos: " + dishScore + ". Total de pontos: " + totalScore);

        if (totalScoreText != null)
            totalScoreText.text = "Total Score: " + totalScore;

        // Para testes, podemos mostrar a avaliação no orderText também:
        if (orderText != null)
            orderText.text += "\nPrato avaliado: " + dishScore + " pontos";

        // Limpa o prato para o próximo pedido
        ClearDish();
        ResetDish();
        // Opcional: gerar um novo pedido com quantidades diferentes
        GenerateNewOrder();
        UpdateUI();
    }

    // Reseta os contadores dos ingredientes
    void ResetDish()
    {
        currentMacarrao = 0;
        currentAlmondegas = 0;
        currentMolho = 0;
        UpdateDishStatusUI();
    }

    // Atualiza a UI do pedido e do prato atual
    void UpdateUI()
    {
        if (orderText != null)
        {
            orderText.text = "Pedido:\n" +
                             "Macarrão: " + requiredMacarrao + "\n" +
                             "Almôndegas: " + requiredAlmondegas + "\n" +
                             "Molho: " + requiredMolho;
        }
        UpdateDishStatusUI();
    }

    // Atualiza o texto que mostra os ingredientes atuais no prato
    void UpdateDishStatusUI()
    {
        if (dishStatusText != null)
        {
            dishStatusText.text = "No Prato:\n" +
                                  "Macarrão: " + currentMacarrao + "\n" +
                                  "Almôndegas: " + currentAlmondegas + "\n" +
                                  "Molho: " + currentMolho;
        }
    }

    // Gera novos valores para o próximo pedido (exemplo com valores randomizados)
    void GenerateNewOrder()
    {
        requiredMacarrao = Random.Range(3, 6);   // 3 a 5
        requiredAlmondegas = Random.Range(4, 7);   // 4 a 6
        requiredMolho = Random.Range(2, 5);        // 2 a 4
    }

    // Limpa os ingredientes (remove os objetos UI filhos do prato) – deve ser chamado após avaliação.
    void ClearDish()
    {
        // Pressupondo que o prato (objeto com tag "Dish") é único na cena
        GameObject dishObject = GameObject.FindGameObjectWithTag("Dish");
        if (dishObject != null)
        {
            foreach (Transform child in dishObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
