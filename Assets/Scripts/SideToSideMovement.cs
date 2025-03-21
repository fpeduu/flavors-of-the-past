using UnityEngine;

public class SideToSideMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Velocidade do movimento
    [SerializeField] private float distance = 3f; // Distância máxima para os lados

    private Vector3 startPosition;
    private float direction = 1f; // 1 para direita, -1 para esquerda
    private float progress = 0f;  // Progresso da movimentação

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Atualiza o progresso baseado na velocidade
        progress += speed * Time.deltaTime * direction;

        // Inverte a direção ao atingir os limites
        if (progress >= distance || progress <= -distance)
        {
            direction *= -1;
            progress = Mathf.Clamp(progress, -distance, distance);
        }

        // Aplica a posição ajustada
        transform.position = new Vector3(startPosition.x + progress, startPosition.y, startPosition.z);
    }
}