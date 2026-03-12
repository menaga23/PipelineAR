using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("References")]
    public PipelineSpawner spawner;
    public ARTouchPlacer touchPlacer;

    [Header("UI Buttons")]
    public Button waterButton;
    public Button ElectricButton;
    public Button sewerButton;
    public Button resetButton;

    [Header("UI Text")]
    public TextMeshProUGUI instructionText;

    void Start()
    {
        waterButton.onClick.AddListener(() => SelectPipe("water"));
        ElectricButton.onClick.AddListener(() => SelectPipe("electric"));
        sewerButton.onClick.AddListener(() => SelectPipe("sewer"));
        resetButton.onClick.AddListener(ResetPipelines);

        instructionText.text = "Point camera at ground, then tap to place pipelines";
    }

    void SelectPipe(string type)
    {
        touchPlacer.ResetPlacement();
        instructionText.text = "Selected: " + type.ToUpper() + " - Tap ground to place!";
    }

    void ResetPipelines()
    {
        touchPlacer.ResetPlacement();
        instructionText.text = "Cleared! Tap ground to place pipelines.";
    }
}