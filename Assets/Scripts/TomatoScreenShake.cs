using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0f);

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }

    // Public method to trigger the shake
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void ShakeByCombo(int combo)
    {
        float duration = 0.1f + combo * 0.05f;
        float magnitude = 0.05f + combo * 0.02f;
        Shake(duration, magnitude);
    }
}
