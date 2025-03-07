using UnityEngine;

public class CuttingLine : MonoBehaviour
{
    public float speed = 2f;
    public float upperLimit = 4f;
    public float lowerLimit = -4f;

    private int direction = 1;

    void Update()
    {
        transform.position += Vector3.up * speed * direction * Time.deltaTime;

        if (transform.position.y >= upperLimit)
            direction = -1;
        else if (transform.position.y <= lowerLimit)
            direction = 1;
    }
}
