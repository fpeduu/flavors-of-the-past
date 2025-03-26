using System;
using UnityEngine;

public class Almondega : MonoBehaviour
{

    public int almondegaPoints = 10;
    private GameManager _gameManager;
    public Action OnHitSomething;

    private Rigidbody2D _projectileRb;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _projectileRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Panela"))
        {
            Debug.Log("Acertou a panela!");
            HitPanela();
        }
        else if (other.CompareTag("Bounds"))
        {
            Debug.Log("Errou a panela!");
            MissPanela();
        }
        OnHitSomething?.Invoke();
        Destroy(this.gameObject);
    }

    private void HitPanela()
    {
        _gameManager.IncrementScore(almondegaPoints);
    }

    private void MissPanela()
    {
        _gameManager.ResetStreak();
    }

    public void Launch(Vector2 direction, float force)
    {
        // Ativa o colisor
        GetComponent<Collider2D>().enabled = true;
        
        // Ativa o rastro
        GetComponent<TrailRenderer>().enabled = true;
        
        // aplica força no projectile
        _projectileRb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
