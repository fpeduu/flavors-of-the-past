using System;
using UnityEngine;

public class Almondega : MonoBehaviour
{

    public int almondegaPoints = 10;
    private GameManager _gameManager;
    public Action OnHitSomething;
    
    [Header("Particle Effects")]
    public GameObject hitEffect;
    
    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip missSound;
    public AudioClip throwSound;
    private AudioSource _audioSource;

    private Rigidbody2D _projectileRb;
    private Collider2D _projectileCollider;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _projectileRb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _projectileCollider = GetComponent<Collider2D>();
        
        hitEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Panela"))
        {
            Debug.Log("Acertou a panela!");
            HitPanela(other);
        }
        else if (other.CompareTag("Bounds"))
        {
            Debug.Log("Errou a panela!");
            MissPanela();
        }
        
    }

    private void HitPanela(Collider2D other)
    {
        HitSomething();
        _gameManager.IncrementScore(almondegaPoints);
        
        _audioSource.PlayOneShot(hitSound);
        
        var effect = Instantiate(hitEffect, other.transform.position, Quaternion.identity);
        effect.SetActive(true);
        Destroy(effect, 1f);
    }

    private void MissPanela()
    {
        HitSomething();
        _gameManager.ResetStreak();
        _audioSource.PlayOneShot(missSound);

    }

    private void HitSomething()
    {
        OnHitSomething?.Invoke();
        Destroy(this.gameObject, 1f);
        GetComponent<Renderer>().enabled = false;
        _projectileCollider.enabled = false;
        _projectileRb.bodyType = RigidbodyType2D.Kinematic;
        _projectileRb.linearVelocity = Vector2.zero;
        
    }

    public void Launch(Vector2 direction, float force)
    {
        // Ativa o colisor
        _projectileCollider.enabled = true;
        
        // Ativa o rastro
        GetComponent<TrailRenderer>().enabled = true;
        
        // aplica força no projectile
        _projectileRb.AddForce(direction * force, ForceMode2D.Impulse);
        
        _audioSource.PlayOneShot(throwSound);
    }
}
