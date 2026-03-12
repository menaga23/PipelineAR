using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARTouchPlacer : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public PipelineSpawner pipelineSpawner;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool pipelinesPlaced = false;

    void Update()
    {
        if (pipelinesPlaced) return;

        if (Input.touchCount > 0 && 
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (raycastManager.Raycast(touch.position, hits, 
                TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                pipelineSpawner.transform.position = hitPose.position;
                pipelineSpawner.SpawnDemoPipelines();
                pipelinesPlaced = true;
            }
        }
    }

    public void ResetPlacement()
    {
        pipelinesPlaced = false;
        pipelineSpawner.ClearAllPipelines();
    }
}