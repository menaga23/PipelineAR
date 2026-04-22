using UnityEngine;
using TMPro;
using MultiSet;

public class ARTouchPlacer : MonoBehaviour
{
    [Header("MultiSet SDK")]
    public MapLocalizationManager mapLocalizationManager;
    public GameObject mapSpace;

    [Header("UI")]
    public GameObject localizingSpinner;
    public TextMeshProUGUI vpsStatusText;

    private bool isLocalized = false;

    void Start()
    {
        // Hide pipes until localized
        if (mapSpace != null)
            mapSpace.SetActive(false);

        if (localizingSpinner != null)
            localizingSpinner.SetActive(true);

        UpdateStatus("Scanning environment...\nPoint camera at the location");

        // Find MapLocalizationManager if not assigned
        if (mapLocalizationManager == null)
            mapLocalizationManager = FindObjectOfType<MapLocalizationManager>();

        if (mapLocalizationManager != null)
        {
            // Subscribe to MultiSet events
            mapLocalizationManager.LocalizationSuccess.AddListener(OnLocalizationSuccess);
            mapLocalizationManager.LocalizationFailure.AddListener(OnLocalizationFailure);

            // Start localization
            mapLocalizationManager.LocalizeFrame();
            Debug.Log("[ARTouchPlacer] VPS localization started...");
        }
        else
        {
            Debug.LogError("[ARTouchPlacer] MapLocalizationManager not found!");
            UpdateStatus("Error: MultiSet SDK not found");
        }
    }

    // Called by MultiSet when environment is recognized
    void OnLocalizationSuccess()
    {
        isLocalized = true;
        Debug.Log("[ARTouchPlacer] Localized! Showing pipes.");

        // MultiSet has already aligned Map Space to real world
        // Just make it visible
        if (mapSpace != null)
            mapSpace.SetActive(true);

        if (localizingSpinner != null)
            localizingSpinner.SetActive(false);

        UpdateStatus("Location found! Pipes are now visible.");

        // Hide status after 3 seconds
        Invoke(nameof(HideStatus), 3f);
    }

    // Called by MultiSet when localization fails
    void OnLocalizationFailure()
    {
        Debug.LogWarning("[ARTouchPlacer] Localization failed. Retrying...");
        UpdateStatus("Scanning...\nMove camera slowly around the area");

        // Retry after 2 seconds
        Invoke(nameof(RetryLocalization), 2f);
    }

    void RetryLocalization()
    {
        if (!isLocalized && mapLocalizationManager != null)
            mapLocalizationManager.LocalizeFrame();
    }

    void UpdateStatus(string message)
    {
        if (vpsStatusText != null)
        {
            vpsStatusText.gameObject.SetActive(true);
            vpsStatusText.text = message;
        }
    }

    void HideStatus()
    {
        if (vpsStatusText != null)
            vpsStatusText.gameObject.SetActive(false);
    }

    // Called by Reset button in UI
    public void ResetSpawn()
    {
        isLocalized = false;

        if (mapSpace != null)
            mapSpace.SetActive(false);

        if (localizingSpinner != null)
            localizingSpinner.SetActive(true);

        UpdateStatus("Scanning environment...\nPoint camera at the location");

        if (mapLocalizationManager != null)
            mapLocalizationManager.LocalizeFrame();
    }

    void OnDestroy()
    {
        if (mapLocalizationManager != null)
        {
            mapLocalizationManager.LocalizationSuccess.RemoveListener(OnLocalizationSuccess);
            mapLocalizationManager.LocalizationFailure.RemoveListener(OnLocalizationFailure);
        }
    }
}