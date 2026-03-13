using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARTouchPlacer : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public PipelineSpawner pipelineSpawner;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (raycastManager.Raycast(touch.position, hits,
                TrackableType.AllTypes))
            {
                Pose hitPose = hits[0].pose;
                pipelineSpawner.transform.position = hitPose.position;
                pipelineSpawner.SpawnDemoPipelines();
            }
        }
    }

    public void ResetPlacement()
    {
        pipelineSpawner.ClearAllPipelines();
    }
}