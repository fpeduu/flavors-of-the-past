using TMPro;
using UnityEngine;

public class EnsureVisibleText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        // Tenta obter o componente TextMeshProUGUI neste GameObject
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
        {
            Debug.LogError("EnsureVisibleText: TextMeshProUGUI component not found on " + gameObject.name);
            return;
        }

        // Garante que o texto esteja totalmente visível (alfa = 1)
        Color currentColor = textComponent.color;
        currentColor.a = 1f;
        textComponent.color = currentColor;
    }

    void Start()
    {
        // Certifique-se de que o objeto está ativo
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
