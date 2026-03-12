using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class PipelineSpawner : MonoBehaviour
{
    [Header("Pipeline Prefabs")]
    public GameObject waterPipePrefab;
    public GameObject electricPipePrefab;
    public GameObject sewerPipePrefab;

    [Header("Label Prefab")]
    public GameObject labelPrefab;

    [Header("Settings")]
    public float depthOffset = -1.5f;

    private List<GameObject> spawnedPipelines = new List<GameObject>();
    private List<GameObject> spawnedLabels = new List<GameObject>();

    public void SpawnPipeline(string type, Vector3 position, 
                              float length, float angle)
    {
        GameObject prefab = GetPrefabByType(type);
        if (prefab == null) return;

        // Spawn pipe
        Vector3 spawnPos = position + new Vector3(0, depthOffset, 0);
        GameObject pipe = Instantiate(prefab, spawnPos, 
                          Quaternion.Euler(0, angle, 90));
        pipe.transform.localScale = new Vector3(0.3f, length, 0.3f);
        spawnedPipelines.Add(pipe);

        // Spawn label above pipe
        if (labelPrefab != null)
        {
            Vector3 labelPos = position + new Vector3(0, 0.5f, 0);
            GameObject label = Instantiate(labelPrefab, labelPos, 
                               Quaternion.identity);
            
            // Set label text
            TextMeshProUGUI tmp = label.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = GetPipelineInfo(type);
            }
            spawnedLabels.Add(label);
        }
    }

    string GetPipelineInfo(string type)
    {
        switch (type.ToLower())
        {
            case "water": return "💧 Water Pipeline\nDepth: 1.5m";
            case "electric": return "⚡ Electric Pipeline\nDepth: 2.0m";
            case "sewer": return "🔧 Sewer Pipeline\nDepth: 3.0m";
            default: return "Pipeline";
        }
    }

    GameObject GetPrefabByType(string type)
    {
        switch (type.ToLower())
        {
            case "water": return waterPipePrefab;
            case "electric": return electricPipePrefab;
            case "sewer": return sewerPipePrefab;
            default: return waterPipePrefab;
        }
    }

    public void SpawnDemoPipelines()
    {
        SpawnPipeline("water", new Vector3(0, 0, 2), 3f, 0f);
        SpawnPipeline("electric", new Vector3(1, 0, 0), 4f, 90f);
        SpawnPipeline("sewer", new Vector3(-1, 0, 1), 2.5f, 45f);
    }

    public void ClearAllPipelines()
    {
        foreach (var pipe in spawnedPipelines)
            Destroy(pipe);
        foreach (var label in spawnedLabels)
            Destroy(label);
        spawnedPipelines.Clear();
        spawnedLabels.Clear();
    }
}