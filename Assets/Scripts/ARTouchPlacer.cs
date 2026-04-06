using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using TMPro;

public class ARTouchPlacer : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public PipelineSpawner pipelineSpawner;
    public TextMeshProUGUI instructionText;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool hasSpawned = false;
    private Vector3 spawnedPosition;

    void Update()
    {
        // Show instruction until plane detected
        if (!hasSpawned)
        {
            if (instructionText != null)
                instructionText.text = "Point camera at floor and tap to place pipelines!";
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            // Only spawn on real detected AR plane
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawnedPosition = hitPose.position;
                pipelineSpawner.SpawnAllAtPosition(spawnedPosition);
                hasSpawned = true;

                if (instructionText != null)
                    instructionText.gameObject.SetActive(false);

                Debug.Log("SPAWNED ON REAL FLOOR AT: " + spawnedPosition);
            }
            else
            {
                if (instructionText != null)
                    instructionText.text = "No floor detected! Move camera slowly on floor!";
                Debug.Log("No AR plane detected - move camera on floor!");
            }
        }
    }

    public void ResetSpawn()
    {
        hasSpawned = false;
        pipelineSpawner.ClearAllPipelines();
        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(true);
            instructionText.text = "Point camera at floor and tap to place pipelines!";
        }
    }
}