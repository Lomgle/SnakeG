using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    public float pulseSpeed = 2.0f;
    public float pulseRange = 0.2f;
    private Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = Mathf.Sin(Time.time * pulseSpeed) * pulseRange;
        transform.localScale = originalScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
