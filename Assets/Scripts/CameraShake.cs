using UnityEngine;

public class CameraShake : SingletonMonoBehavior<CameraShake>
{
    private float shakeDuration = 0f;  // How long the shake lasts
    private float shakeIntensity = 0.1f;  // Intensity of the shake
    private Vector3 originalPosition;  // Original position of the camera
    private float shakeTimeRemaining;
    [SerializeField] private float basicShakeDuration, basicShakeAmplitude;
    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // Randomize the camera position for the shake effect
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;

            // Decrease the remaining shake time
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position back to the original
            transform.position = originalPosition;
        }
    }

    // Call this method to trigger a custom shake
    public void TriggerShake(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeTimeRemaining = duration;
    }
    // Call this method to trigger a basic small shake
    public void TriggerShake()
    {
        shakeDuration = basicShakeDuration;
        shakeIntensity = basicShakeAmplitude;
        shakeTimeRemaining = basicShakeDuration;
    }
}
