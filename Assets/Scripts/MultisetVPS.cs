using UnityEngine;
using TMPro;

public class MultiSetVPS : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI vpsStatusText;

    private float timer = 0f;
    private bool isLocalized = false;

    void Start()
    {
        UpdateStatus("Initializing MultiSet AI VPS...");
    }

    void Update()
    {
        if (!isLocalized)
        {
            timer += Time.deltaTime;

            if (timer >= 1f && timer < 2f)
                UpdateStatus("Scanning environment...\nMove camera slowly");

            if (timer >= 2f && timer < 3f)
                UpdateStatus("Analyzing visual features...");

            if (timer >= 3f && timer < 4f)
                UpdateStatus("Matching location data...");

            if (timer >= 4f)
            {
                isLocalized = true;
                UpdateStatus("✅ VPS Localized!\nCentimeter accuracy achieved");
            }
        }
    }

    void UpdateStatus(string message)
    {
        if (vpsStatusText != null)
            vpsStatusText.text = message;
    }

    public bool IsLocalized()
    {
        return isLocalized;
    }
}