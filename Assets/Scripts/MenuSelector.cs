using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public RectTransform selector;   // The cookie Image serving as your selector
    public RectTransform jogarText;  // RectTransform for "Jogar" text
    public RectTransform sairText;   // RectTransform for "Sair" text

    // How far to the left of the text the cookie should appear
    // (negative X = left; positive X = right)
    public float xOffset = -50f;

    private int selectedIndex = 0;
    private RectTransform[] options;

    void Start()
    {
        // Put the text elements in an array so we can switch easily
        options = new RectTransform[] { jogarText, sairText };

        // Move selector to the first option’s position
        UpdateSelectorPosition();
    }

    void Update()
    {
        // Cycle to next option (Down/Right arrow)
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = (selectedIndex + 1) % options.Length;
            UpdateSelectorPosition();
        }
        // Cycle to previous option (Up/Left arrow)
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            UpdateSelectorPosition();
        }

        // Confirm selection with Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelection();
        }
    }

    void UpdateSelectorPosition()
    {
        // Make sure the selector is a child of the same parent as the text,
        // so anchoredPosition refers to the same local coordinate space
        selector.SetParent(options[selectedIndex].parent, false);

        // Get the text’s anchoredPosition
        Vector2 textPos = options[selectedIndex].anchoredPosition;

        // Shift left by xOffset so the cookie is to the left
        float newX = textPos.x + xOffset;
        float newY = textPos.y;  // keep the same vertical alignment as the text

        // Assign that to the selector’s anchoredPosition
        selector.anchoredPosition = new Vector2(newX, newY);
    }

    void ConfirmSelection()
    {
        if (selectedIndex == 0)
        {
            // "Jogar" selected
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            // "Sair" selected
            Application.Quit();
        }
    }
}
