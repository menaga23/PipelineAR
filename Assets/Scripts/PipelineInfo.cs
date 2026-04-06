using UnityEngine;
using TMPro;

public class PipelineInfo : MonoBehaviour
{
    [Header("UI References")]
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public ARTouchPlacer touchPlacer;

    void Start()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    void Update()
    {
        if (Camera.main == null) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch    = Input.GetTouch(0);
            Ray ray        = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit[] hits = Physics.RaycastAll(ray, 20f);

            Debug.Log("TAP! Hits found: " + hits.Length);

            bool hitPipe = false;

            foreach (RaycastHit hit in hits)
            {
                string name = hit.collider.gameObject.name.ToLower();
                Debug.Log("Hit: " + name);

                if (name.Contains("water"))
                {
                    ShowInfo("WATER PIPELINE",
                        "Type: Water Supply\n" +
                        "Depth: 1.5 meters\n" +
                        "Material: PVC\n" +
                        "Diameter: 200mm\n" +
                        "Installed: 2018\n" +
                        "Owner: City Water Board");
                    hitPipe = true;
                    BlockTouchPlacer();
                    break;
                }
                else if (name.Contains("electric"))
                {
                    ShowInfo("ELECTRIC PIPELINE",
                        "Type: Electric Conduit\n" +
                        "Depth: 2.0 meters\n" +
                        "Material: Steel\n" +
                        "Diameter: 150mm\n" +
                        "Installed: 2020\n" +
                        "Owner: City Electric Board");
                    hitPipe = true;
                    BlockTouchPlacer();
                    break;
                }
                else if (name.Contains("sewer"))
                {
                    ShowInfo("SEWER PIPELINE",
                        "Type: Sewage System\n" +
                        "Depth: 3.0 meters\n" +
                        "Material: Concrete\n" +
                        "Diameter: 300mm\n" +
                        "Installed: 2015\n" +
                        "Owner: City Municipal Corp");
                    hitPipe = true;
                    BlockTouchPlacer();
                    break;
                }
            }

            if (!hitPipe)
            {
                Debug.Log("No pipe tapped");
                HideInfo();
            }
        }
    }

    void ShowInfo(string title, string details)
    {
        infoPanel.SetActive(true);
        infoText.text = "<b>" + title + "</b>\n\n" + details;
    }

    public void HideInfo()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    void BlockTouchPlacer()
    {
        if (touchPlacer != null)
        {
            touchPlacer.enabled = false;
            Invoke(nameof(ReEnable), 0.1f);
        }
    }

    void ReEnable()
    {
        if (touchPlacer != null) touchPlacer.enabled = true;
    }
}