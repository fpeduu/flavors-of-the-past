using System;
using System.Collections;
using UnityEngine;

public class IngredientMover : MonoBehaviour
{
    public float speed = 3f;
    public float fallSpeed = 1f;
    private bool movingRight = true;
    private bool isFalling = false;
    private bool isPlaced = false;
    public float horizontalLimit = 2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlaced) return; // ta no prato

        if (isFalling)
        {
            // ta caindo
            rb.MovePosition(rb.position + Vector2.down * (fallSpeed * Time.deltaTime));
        }

        else
        {
            // ta andando pros lados
            
            MoveSide();
        }

        
    }

    void MoveSide()
    {
        // Movimento lateral
        float moveDirection = movingRight ? 1 : -1;
        // transform.Translate(Vector3.right * (moveDirection * speed * Time.deltaTime));

        rb.MovePosition(rb.position + Vector2.right * (moveDirection * speed * Time.deltaTime));
        // Inverter direção ao tocar nas bordas da tela
        if (transform.position.x > horizontalLimit) movingRight = false;
        if (transform.position.x < -horizontalLimit) movingRight = true;

        // Parar ao clicar
        if (Input.GetMouseButtonDown(0)) // Clique do mouse ou toque
        {
            isFalling = true;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlaced) return;
        Debug.Log("colidiu com algo");
        isPlaced = true;
        StackManager.Instance.CheckPlacement(transform);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}