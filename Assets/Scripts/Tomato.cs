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
    public Sprite rottenTomatoSprite;
    public Sprite cutTomatoSprite;
    public Sprite smashedTomatoSprite;
    private SpriteRenderer spriteRenderer;
    private bool isRotten;
    public bool hasBeenCut = false;

    [Header("Audio")]
    public AudioClip hitGroundSound;
    public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isRotten = Random.value < 0.1f; // 10% chance of being rotten
        if (isRotten && rottenTomatoSprite != null)
        {
            spriteRenderer.sprite = rottenTomatoSprite;
        }

        ApplyInitialForces();
        InitializeAudioSource();
    }

    void Update()
    {
        spawnProtectionTimer += Time.deltaTime;
        if (spawnProtectionTimer < spawnProtectionTime) return;

        if (hasHitGround || hasBeenCut) return;

        CheckGroundCollision();
    }

    private void ApplyInitialForces()
    {
        if (rb != null)
        {
            float verticalForce = Random.Range(minUpwardForce, maxUpwardForce);
            float horizontalForce = Random.Range(minLateralForce, maxLateralForce);
            rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
        }
    }

    private void InitializeAudioSource()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void CheckGroundCollision()
    {
        if (transform.position.y <= -4f)
        {
            HandleGroundHit();
        }
    }

    private void HandleGroundHit()
    {
        hasHitGround = true;
        UpdateSprite(smashedTomatoSprite);
        PlaySound(hitGroundSound);
        StopMovement();

        if (!isRotten)
        {
            TomatoGameManager.Instance.ResetCombo();
            TomatoGameManager.Instance.RemoveScore(5);
        }

        Destroy(gameObject, 2f);
    }

    public void Cut()
    {
        if (hasHitGround || hasBeenCut) return;

        hasBeenCut = true;
        UpdateSprite(cutTomatoSprite);

        if (!isRotten)
        {
            TomatoGameManager.Instance.AddScore(10); // Normal cut with combo
        }
        else
        {
            TomatoGameManager.Instance.ResetCombo(); // Reset combo for rotten tomatoes
            TomatoGameManager.Instance.RemoveScore(-10);
        }

        Destroy(gameObject, 1f);
    }

    private float difficultyMultiplier = 0f;
    public void SetDifficultyMultiplier(float multiplier)
    {
        difficultyMultiplier = Mathf.Clamp01(multiplier);
    }


    private void UpdateSprite(Sprite newSprite)
    {
        if (newSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void StopMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }
}