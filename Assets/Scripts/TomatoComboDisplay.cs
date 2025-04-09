using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI comboText;

    [Header("Visual Settings")]
    [SerializeField] private Color[] comboColors;
    [SerializeField] private float maxComboScale = 1.5f;

    private float comboDisplayTimer;
  [SerializeField] private float comboDisplayDuration = 1.5f;

    private void Start()
    {
        TomatoGameManager.Instance.OnComboChanged.AddListener(UpdateComboDisplay);
        comboText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (comboText.gameObject.activeSelf)
        {
            comboDisplayTimer += Time.deltaTime;
            if (comboDisplayTimer >= comboDisplayDuration)
            {
                comboText.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateComboDisplay(int combo)
    {
        if (combo <= 0)
        {
            return;
        }

        comboText.gameObject.SetActive(true);
        comboDisplayTimer = 0f;

        comboText.text = $"{combo}x COMBO!";

        comboText.transform.localScale = Vector3.one * maxComboScale;
    }
}