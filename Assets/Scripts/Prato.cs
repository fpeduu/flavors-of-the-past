using UnityEngine;

public class Prato : MonoBehaviour
{
    public float velocidade = 3f;
    public float limiteEsquerda = -4f, limiteDireita = 4f;

    void Update()
    {
        transform.position += Vector3.right * velocidade * Time.deltaTime;
        
        if (transform.position.x > limiteDireita || transform.position.x < limiteEsquerda)
        {
            velocidade *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Macarrao"))
        {
            Destroy(other.gameObject); // O macarrão foi coletado com sucesso
        }
    }
}