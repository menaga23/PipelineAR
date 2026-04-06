using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PipelineSpawner : MonoBehaviour
{
    [Header("Label Prefab (optional)")]
    public GameObject labelPrefab;

    private List<GameObject> spawnedPipelines = new List<GameObject>();
    private List<GameObject> spawnedLabels = new List<GameObject>();

    public void SpawnAllAtPosition(Vector3 tapPoint)
    {
        ClearAllPipelines();
        SpawnColoredPipe("water",    tapPoint);
        SpawnColoredPipe("electric", tapPoint);
        SpawnColoredPipe("sewer",    tapPoint);
        Debug.Log("ALL 3 PIPELINES SPAWNED");
    }

    void SpawnColoredPipe(string type, Vector3 tapPoint)
    {
        GameObject pipe = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pipe.name = type + "Pipeline";

        Vector3 offset    = Vector3.zero;
        Color pipeColor   = Color.white;
        string labelText  = "";

        switch (type.ToLower())
        {
            case "water":
                offset    = new Vector3(0f, 0.02f, -0.3f);
                pipeColor = Color.blue;
                labelText = "Water Pipeline\nDepth: 1.5m\nMaterial: PVC\nOwner: City Water Board";
                break;
            case "electric":
                offset    = new Vector3(0f, 0.02f, 0f);
                pipeColor = Color.yellow;
                labelText = "Electric Pipeline\nDepth: 2.0m\nMaterial: Steel\nOwner: City Electric Board";
                break;
            case "sewer":
                offset    = new Vector3(0f, 0.02f, 0.3f);
                pipeColor = new Color(0.6f, 0.3f, 0f);
                labelText = "Sewer Pipeline\nDepth: 3.0m\nMaterial: Concrete\nOwner: City Municipal Corp";
                break;
        }

        pipe.transform.position   = tapPoint + offset;
        pipe.transform.rotation   = Quaternion.Euler(0, 0, 90);
        pipe.transform.localScale = new Vector3(0.03f, 0.25f, 0.03f);

        // Apply URP color
        Renderer rend = pipe.GetComponent<Renderer>();
        Material mat  = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.SetColor("_BaseColor", pipeColor);
        rend.material = mat;

        // Fixed collider
        Collider existing = pipe.GetComponent<Collider>();
        if (existing != null) DestroyImmediate(existing);
        BoxCollider box = pipe.AddComponent<BoxCollider>();
        box.size   = new Vector3(1f, 1f, 1f);
        box.center = Vector3.zero;

        spawnedPipelines.Add(pipe);
        Debug.Log("SPAWNED: " + pipe.name);

        if (labelPrefab != null)
        {
            Vector3 labelPos = tapPoint + offset + new Vector3(0, 0.15f, 0);
            GameObject label = Instantiate(labelPrefab, labelPos, Quaternion.identity);
            TextMeshProUGUI tmp = label.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) tmp.text = labelText;
            spawnedLabels.Add(label);
        }
    }

    public void ClearAllPipelines()
    {
        foreach (var pipe in spawnedPipelines)
            if (pipe != null) Destroy(pipe);
        foreach (var label in spawnedLabels)
            if (label != null) Destroy(label);
        spawnedPipelines.Clear();
        spawnedLabels.Clear();
    }
}