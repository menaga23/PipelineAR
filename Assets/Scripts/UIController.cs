using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Info Panel")]
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;

    [Header("References")]
    public ARTouchPlacer touchPlacer;
    public PipelineSpawner spawner;

    void Start()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    public void ShowWaterInfo()
    {
        // ✅ Always closes first then reopens
        infoPanel.SetActive(false);
        infoPanel.SetActive(true);
        infoText.text =
            "<b>💧 WATER PIPELINE</b>\n\n" +
            "Type: Water Supply\n" +
            "Depth: 1.5 meters\n" +
            "Material: PVC\n" +
            "Diameter: 200mm\n" +
            "Installed: 2018\n" +
            "Owner: City Water Board";
    }

    public void ShowElectricInfo()
    {
        infoPanel.SetActive(false);
        infoPanel.SetActive(true);
        infoText.text =
            "<b>⚡ ELECTRIC PIPELINE</b>\n\n" +
            "Type: Electric Conduit\n" +
            "Depth: 2.0 meters\n" +
            "Material: Steel\n" +
            "Diameter: 150mm\n" +
            "Installed: 2020\n" +
            "Owner: City Electric Board";
    }

    public void ShowSewerInfo()
    {
        infoPanel.SetActive(false);
        infoPanel.SetActive(true);
        infoText.text =
            "<b>🟤 SEWER PIPELINE</b>\n\n" +
            "Type: Sewage System\n" +
            "Depth: 3.0 meters\n" +
            "Material: Concrete\n" +
            "Diameter: 300mm\n" +
            "Installed: 2015\n" +
            "Owner: City Municipal Corp";
    }

    public void HideInfo()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    public void OnResetButton()
    {
        touchPlacer.ResetSpawn();
        HideInfo();
    }
}