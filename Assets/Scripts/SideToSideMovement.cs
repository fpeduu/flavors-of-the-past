using UnityEngine;
using UnityEngine.Serialization;

public class SideToSideMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 2f; // Velocidade do movimento
    [SerializeField] private float distance = 3f; // Distância máxima para os lados
    [SerializeField] private bool increaseSpeedOverTime = false;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float timeToReachMaxSpeed = 1f;
    
    
    private Vector3 startPosition;
    private float currentTime;
    private float direction = 1f; // 1 para direita, -1 para esquerda
    private float progress = 0f;  // Progresso da movimentação
    private float currentSpeed;

    private void Start()
    {
        startPosition = transform.position;
        currentTime = 0;
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        if (increaseSpeedOverTime)
        {
            currentTime += Time.deltaTime;
            
            currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, currentTime / timeToReachMaxSpeed);
            
            if (currentTime >= timeToReachMaxSpeed)
            {
                increaseSpeedOverTime = false;
            }
        }
        
        // Atualiza o progresso baseado na velocidade
        progress += currentSpeed * Time.deltaTime * direction;

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