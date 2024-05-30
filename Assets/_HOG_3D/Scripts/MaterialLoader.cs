using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialLoader : MonoBehaviour
{
    public Material[] materials; // Array to hold the materials assigned in the editor
    private Renderer[] renderers; // Array to hold renderers of the object and its children
    private Material defaultMaterial; // Default material of the object

    void Start()
    {
        // Get reference to the renderers of the object and its children
        renderers = GetComponentsInChildren<Renderer>();

        // Store the default material of the object
        defaultMaterial = renderers[0].material;

        // Check if materials array is not null and contains at least one material
        if (materials == null || materials.Length == 0)
        {
            Debug.LogError("No materials assigned or materials array is empty!");
            return;
        }

        // Apply the default material to the object
        foreach (Renderer renderer in renderers)
        {
            renderer.material = defaultMaterial;
        }
    }

    public void ReloadMaterials()
    {
        // Check if materials array is not null and contains at least one material
        if (materials == null || materials.Length == 0)
        {
            Debug.LogError("No materials assigned or materials array is empty!");
            return;
        }

        // Apply a random material from the materials array to the object and its children
        foreach (Renderer renderer in renderers)
        {
            int randomIndex = Random.Range(0, materials.Length);
            renderer.material = materials[randomIndex];
        }
    }

    void Update()
    {
        // Check for key press to simulate space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SimulateSpaceKeyPress();
        }
    }

    public void SimulateSpaceKeyPress()
    {
        ReloadMaterials();
    }
}
