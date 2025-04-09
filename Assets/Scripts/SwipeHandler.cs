using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip cutSound;
    public AudioSource audioSource;

    [Header("Swipe Effect")]
    public GameObject swipeEffectPrefab; // Assign a prefab with TrailRenderer
    private GameObject currentSwipeEffect;
    private Vector3 lastMousePosition;
    private bool isSwiping;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("No AudioSource found on SwipeHandler GameObject. Please add one.");
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSwipe();
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            ContinueSwipe();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSwipe();
        }
    }

    void StartSwipe()
    {
        isSwiping = true;
        lastMousePosition = GetMouseWorldPosition();
        currentSwipeEffect = Instantiate(swipeEffectPrefab, lastMousePosition, Quaternion.identity);

        TrailRenderer trail = currentSwipeEffect.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.sortingLayerName = "Foreground";
            trail.sortingOrder = 10;
        }
    }

    void ContinueSwipe()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        if (currentSwipeEffect != null)
        {
            currentSwipeEffect.transform.position = mousePos;
        }
        CheckForTomatoHit(mousePos);
        lastMousePosition = mousePos;
    }

    void EndSwipe()
    {
        isSwiping = false;
        if (currentSwipeEffect != null)
        {
            TrailRenderer trailRenderer = currentSwipeEffect.GetComponent<TrailRenderer>();
            float destroyDelay = trailRenderer != null ? trailRenderer.time : 1f;
            Destroy(currentSwipeEffect, destroyDelay);
        }
    }

    void CheckForTomatoHit(Vector3 mousePos)
    {
        Collider2D hit = Physics2D.OverlapPoint(new Vector2(mousePos.x, mousePos.y));
        if (hit != null && hit.CompareTag("Tomato"))
        {
            Tomato tomato = hit.GetComponent<Tomato>();
            if (tomato != null)
            {
                if (!tomato.hasBeenCut && !tomato.hasHitGround)
                {
                    audioSource.PlayOneShot(cutSound);
                    tomato.Cut();
                }
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
}