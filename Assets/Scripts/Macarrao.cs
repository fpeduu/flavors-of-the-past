using UnityEngine;

public class Macarrao : MonoBehaviour
{
    public float velocidadeQueda = 2f;
    
    void Update()
    {
        transform.position += Vector3.down * velocidadeQueda * Time.deltaTime;
    }
}