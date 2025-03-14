using UnityEngine;

public class Tomato : MonoBehaviour
{
    private bool hasHitGround = false;
    private float spawnProtectionTimer = 0f;
    private Rigidbody2D rb;

    [Header("Force Settings")]
    [Tooltip("Minimum upward force when spawned")]
    public float minUpwardForce = .5f;
    [Tooltip("Maximum upward force when spawned")]
    public float maxUpwardForce = 4f;

    [Tooltip("Minimum lateral (sideways) force")]
    public float minLateralForce = -3f;
    [Tooltip("Maximum lateral (sideways) force")]
    public float maxLateralForce = 3f;

    [Header("Settings")]
    [Tooltip("Delay before ground detection starts")]
    public float spawnProtectionTime = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float verticalForce = Random.Range(minUpwardForce, maxUpwardForce);
            float horizontalForce = Random.Range(minLateralForce, maxLateralForce);

            rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        spawnProtectionTimer += Time.deltaTime;
        if (spawnProtectionTimer < spawnProtectionTime) return;

        if (hasHitGround) return;

        if (transform.position.y <= -4f)
        {
            hasHitGround = true;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            TomatoGameManager.Instance.UpdateScore(-5);
            Destroy(gameObject, 2f);
        }
    }

    public void Cut()
    {
        if (hasHitGround) return;
        TomatoGameManager.Instance.UpdateScore(10);
        Destroy(gameObject);
    }
}