using UnityEngine;

public class Tomato : MonoBehaviour
{
    public bool hasHitGround = false;
    private float spawnProtectionTimer = 0f;
    private Rigidbody2D rb;

    [Header("Force Settings")]
    public float minUpwardForce = .5f;
    public float maxUpwardForce = 4f;
    public float minLateralForce = -3f;
    public float maxLateralForce = 3f;

    [Header("Settings")]
    public float spawnProtectionTime = 0.2f;

    [Header("Tomato Sprite")]
    public Sprite rottenTomatoSprite;  // Reference to the rotten tomato sprite
    public Sprite cutTomatoSprite;  // Reference to the cut tomato sprite
    public Sprite smashedTomatoSprite;
    private SpriteRenderer spriteRenderer;
    private bool isRotten;
    public bool hasBeenCut = false;

    [Header("Audio")]
    public AudioClip hitGroundSound;  // Sound when the tomato hits the ground
    public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component

        isRotten = Random.value < 0.1f; // 10% chance of being rotten

        if (isRotten)
        {
            if (rottenTomatoSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = rottenTomatoSprite;  // Change the sprite
            }
        }

        if (rb != null)
        {
            float verticalForce = Random.Range(minUpwardForce, maxUpwardForce);
            float horizontalForce = Random.Range(minLateralForce, maxLateralForce);

            rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        spawnProtectionTimer += Time.deltaTime;
        if (spawnProtectionTimer < spawnProtectionTime) return;

        if (hasHitGround || hasBeenCut) return;

        if (transform.position.y <= -4f)
        {
            hasHitGround = true;
            if (cutTomatoSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = smashedTomatoSprite;  // Change the sprite
            }

            if (hitGroundSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitGroundSound);
            }

            rb.linearVelocity = Vector2.zero; // Reset velocity when hitting the ground
            rb.gravityScale = 0;
            if (!isRotten)
            {
              TomatoGameManager.Instance.UpdateScore(-5);
            }
            Destroy(gameObject, 2f);
        }
    }

    public void Cut()
    {
        if (hasHitGround || hasBeenCut) return;

        hasBeenCut = true;  // Set the flag to true

        // Change the sprite to the cut version
        if (cutTomatoSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = cutTomatoSprite;  // Change the sprite
        }

        if (!isRotten)
        {
          TomatoGameManager.Instance.UpdateScore(10);
        }
        else
        {
          TomatoGameManager.Instance.UpdateScore(-10);
        }
        Destroy(gameObject, 1f); // Delay destruction after the cut effect
    }
}
