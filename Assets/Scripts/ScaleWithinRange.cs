using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ScaleWithinRange : MonoBehaviour
{
    [SerializeField] private GameObject objectToScale;
    public float minScale = 0.1f;
    public float maxScale = 1.0f;
    public float speed;
    public bool isScaling = true;

    private float scaleProgress = 0f;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = objectToScale.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isScaling)
            return;
        
        scaleProgress += Time.deltaTime * speed;
        float t = Mathf.PingPong(scaleProgress / 10f, 1);
        float scale = Mathf.Lerp(minScale, maxScale, t);
        Vector3 newScale = new (originalScale.x, scale, originalScale.z);
        objectToScale.transform.localScale = newScale;
    }
}
