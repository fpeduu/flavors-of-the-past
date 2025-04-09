using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Image comboMeter;
    [SerializeField] private Animator comboAnimator;

    [Header("Visual Settings")]
    [SerializeField] private Color[] comboColors;
    [SerializeField] private float maxComboScale = 1.5f;
    [SerializeField] private ParticleSystem comboParticles;

    private void Start()
    {
        ScoreManager.Instance.OnComboChanged.AddListener(UpdateComboDisplay);
        comboText.gameObject.SetActive(false);
    }

    private void UpdateComboDisplay(int combo)
    {
        if (combo <= 0)
        {
            comboText.gameObject.SetActive(false);
            comboParticles.Stop();
            return;
        }

        comboText.gameObject.SetActive(true);
        comboText.text = $"{combo}x COMBO!";

        // Animate combo text
        comboAnimator.Play("ComboPop", -1, 0f);

        // Update colors based on combo level
        int colorIndex = Mathf.Clamp(combo - 1, 0, comboColors.Length - 1);
        comboText.color = comboColors[colorIndex];

        // Update combo meter
        float fillAmount = (float)combo / ScoreManager.Instance.MaxComboMultiplier;
        comboMeter.fillAmount = fillAmount;

        // Particle effects
        if (combo % 5 == 0)
        {
            comboParticles.Play();
        }
    }
}