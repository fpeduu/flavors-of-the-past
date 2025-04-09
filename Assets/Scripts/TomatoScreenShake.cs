using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    private float shakeDuration;
    private float shakeMagnitude;
    private float dampingSpeed;
    private Vector3 initialPosition;

    private void Awake()
    {
        Instance = this;
        initialPosition = transform.localPosition;
    }

    public void TriggerShake(float duration, float magnitude, float damping)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        dampingSpeed = damping;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
}