using UnityEngine;
using System.Collections;
public class PanelaAlmondega : MonoBehaviour
{
    private Animator animator; // Referência ao Animator
    private Collider2D panCollider; // Referência ao Collider da tampa
    public float interval = 5f; // Intervalo entre abrir e fechar

    private void Start()
    {

        animator = GetComponent<Animator>();
        panCollider = GetComponent<Collider2D>();
        StartCoroutine(ToggleLidRoutine());
    }

    public void CloseLid()
    {
        panCollider.enabled = false; // Desativa o collider no início da animação de abrir
        animator.SetTrigger("CloseLid"); // Aciona a animação de fechar
    }

    public void OpenLid()
    {
        panCollider.enabled = true; // Ativa o collider no início da animação de abrir
        animator.SetTrigger("OpenLid"); // Aciona a animação de abrir
    }

    private IEnumerator ToggleLidRoutine()
    {
        while (true)
        {
            CloseLid();
            yield return new WaitForSeconds(interval);
            OpenLid();
            yield return new WaitForSeconds(interval);
        }
    }
}
