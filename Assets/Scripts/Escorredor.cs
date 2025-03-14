using System;
using TMPro;
using UnityEngine;

public class Escorredor : MonoBehaviour
{
    public static Escorredor instance;
    
    public float velocidade = 5f;
    public Transform pontoDeSoltar;
    private bool carregandoMacarrao = false;

    private int score = 0;
    public TextMeshProUGUI scoreText;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = "Pontuação: " + score;
    }

    void Update()
    {
        float movimento = Input.GetAxis("Horizontal") * velocidade * Time.deltaTime;
        transform.position += new Vector3(movimento, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && carregandoMacarrao)
        {
            SoltarMacarrao();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Macarrao"))
        {
            carregandoMacarrao = true;
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            var otherRb = other.GetComponent<Rigidbody2D>();
            otherRb.bodyType = RigidbodyType2D.Kinematic;
            otherRb.linearVelocity = Vector2.zero;
            
        }
    }

    public void SoltarMacarrao()
    {
        if (carregandoMacarrao)
        {
            Transform macarrao = transform.GetChild(1);
            macarrao.SetParent(null);
            macarrao.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            macarrao.position = pontoDeSoltar.position;
            carregandoMacarrao = false;
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Pontuação: " + score;
    }
}