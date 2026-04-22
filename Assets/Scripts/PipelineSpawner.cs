using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PipelineSpawner : MonoBehaviour
{
    public GameObject labelPrefab;
    private List<GameObject> spawnedPipelines = new List<GameObject>();
    private List<GameObject> spawnedLabels = new List<GameObject>();

    public void SpawnAtPosition(Vector3 position)
    {
        ClearAllPipelines();
        SpawnColoredPipe("water", position);
        SpawnColoredPipe("electric", position);
        SpawnColoredPipe("sewer", position);
        Debug.Log("ALL 3 PIPELINES SPAWNED");
    }

    void SpawnColoredPipe(string type, Vector3 position)
    {
        GameObject pipe = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pipe.name = type + "Pipeline";

        Vector3 offset = Vector3.zero;
        Color pipeColor = Color.white;
        string labelText = "";

        switch (type.ToLower())
        {
            case "water":
                offset = new Vector3(-0.3f, 0f, 0f);
                pipeColor = Color.blue;
                labelText = "Water Pipeline\nDepth: 1.5m\nMaterial: PVC";
                break;
            case "electric":
                offset = new Vector3(0f, 0f, 0f);
                pipeColor = Color.yellow;
                labelText = "Electric Pipeline\nDepth: 2.0m\nMaterial: Steel";
                break;
            case "sewer":
                offset = new Vector3(0.3f, 0f, 0f);
                pipeColor = new Color(0.6f, 0.3f, 0f);
                labelText = "Sewer Pipeline\nDepth: 3.0m\nMaterial: Concrete";
                break;
        }

        pipe.transform.position = position + offset;
        pipe.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        pipe.transform.localScale = new Vector3(0.05f, 0.3f, 0.05f);

        Renderer rend = pipe.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.SetColor("_BaseColor", pipeColor);
        rend.material = mat;

        Collider existing = pipe.GetComponent<Collider>();
        if (existing != null) DestroyImmediate(existing);
        BoxCollider box = pipe.AddComponent<BoxCollider>();
        box.size = new Vector3(1f, 1f, 1f);
        box.center = Vector3.zero;

        spawnedPipelines.Add(pipe);

        if (labelPrefab != null)
        {
            Vector3 labelPos = position + offset + new Vector3(0, 0.2f, 0);
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