using UnityEngine;

public class RotateWithinRange : MonoBehaviour
{
    [SerializeField] private GameObject objectToRotate;
    public float minAngle;
    public float maxAngle;
    public float speed;
    public bool isRotating = true;

    private float rotationProgress = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (!isRotating)
            return;
        
        rotationProgress += Time.deltaTime * speed;
        float t = Mathf.PingPong(rotationProgress / 100f, 1);
        float angle = Mathf.Lerp(minAngle, maxAngle, t);
        objectToRotate.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
