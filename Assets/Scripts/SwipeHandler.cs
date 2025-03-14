using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip cutSound;
    public AudioSource audioSource;

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
        // Convert mouse position with explicit Z value
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)
        );
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        Collider2D hit = Physics2D.OverlapPoint(mousePos2D);

        if (hit != null && hit.CompareTag("Tomato"))
        {
            Tomato tomato = hit.GetComponent<Tomato>();
            if (tomato != null)
            {
                Debug.Log("Hit a tomato!");
                Debug.Log($"Tomato position: {tomato.transform.position.y}");

                if (!tomato.hasBeenCut && !tomato.hasHitGround) {
                  audioSource.PlayOneShot(cutSound);
                  tomato.Cut();
                }
            }
            else
            {
                Debug.LogError("Tomato script missing on: " + hit.gameObject.name);
            }
        }
    }
}