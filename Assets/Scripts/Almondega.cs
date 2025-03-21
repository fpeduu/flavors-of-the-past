using System;
using UnityEngine;

public class Almondega : MonoBehaviour
{

    public event Action<int> OnHitPanela;
    public int almondegaPoints = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Panela"))
        {
            Debug.Log("Acertou a panela!");
            OnHitPanela?.Invoke(almondegaPoints);
        }
        else if (other.CompareTag("Bounds"))
        {
            Debug.Log("Errou a panela!");
        }
        
        Destroy(this.gameObject);
    }
}
