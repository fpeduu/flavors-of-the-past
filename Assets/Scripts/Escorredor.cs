using UnityEngine;

public class Escorredor : MonoBehaviour
{
    public float velocidade = 5f;
    public Transform pontoDeSoltar;
    private bool carregandoMacarrao = false;

    void Update()
    {
        float movimento = Input.GetAxis("Horizontal") * velocidade * Time.deltaTime;
        transform.position += new Vector3(movimento, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Macarrao"))
        {
            carregandoMacarrao = true;
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void SoltarMacarrao()
    {
        if (carregandoMacarrao)
        {
            Transform macarrao = transform.GetChild(0);
            macarrao.SetParent(null);
            macarrao.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            macarrao.position = pontoDeSoltar.position;
            carregandoMacarrao = false;
        }
    }
}