using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Added for scene management

public class RecipeSelector : MonoBehaviour
{
    public RectTransform pizzaPointer;   // The UI Image for the pizza slice pointer
    public RectTransform[] recipeFrames;   // The frames for each recipe

    private int currentIndex = 0;

    // Animation parameters for the pointer's oscillation
    public float baseOffsetY = -100f;           // Base offset: 100 pixels below the frame
    public float oscillationAmplitude = 5f;     // How much the pointer moves up and down
    public float oscillationFrequency = 2f;     // Speed of the oscillation
    // Set stutter interval to update ~6 times per second (1/6 ≈ 0.1667 seconds)
    public float stutterInterval = 0.1667f;

    void Start()
    {
        UpdatePointerPosition();
    }

    void Update()
    {
        // Navigate between recipes with arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex = (currentIndex + 1) % recipeFrames.Length;
            UpdatePointerPosition();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex = (currentIndex - 1 + recipeFrames.Length) % recipeFrames.Length;
            UpdatePointerPosition();
        }

        // Update the pointer position every frame to include the stuttered oscillation
        UpdatePointerPosition();

        // Confirm selection with Enter (if needed)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelection();
        }
    }

    void UpdatePointerPosition()
    {
        // Base position: current recipe frame's position plus the fixed vertical offset
        Vector3 basePosition = recipeFrames[currentIndex].position + new Vector3(0, baseOffsetY, 0);

        // Quantize time to create a stuttering effect at 6 fps
        float discreteTime = Mathf.Floor(Time.time / stutterInterval) * stutterInterval;

        // Calculate the oscillation using the quantized time value
        float oscillation = oscillationAmplitude * Mathf.Sin(discreteTime * oscillationFrequency);

        // Set the pointer's position to the base position plus the oscillation offset
        pizzaPointer.position = basePosition + new Vector3(0, oscillation, 0);
    }

    void ConfirmSelection()
    {
        Debug.Log("Selected recipe index: " + currentIndex);

        // If the selected index is 1, load the "MacarraoMinigame" scene
        if (currentIndex == 1)
        {
            SceneManager.LoadScene("MacarraoMinigame");
        }
        else
        {
            // Optionally, handle other indexes or log a message
            Debug.Log("No scene assigned for this recipe.");
        }
    }
}
