using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerShelf : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Array to hold the prefabs assigned in the editor
    public int numberOfObjectsToSpawn = 10; // Number of objects to spawn
    public float spawnAreaWidth = 5f; // Width of the spawn area along the X axis
    public float spawnAreaHeight = 5f; // Height of the spawn area along the Y axis
    public float spawnZPosition = 0f; // Z position for spawning objects
    public float minDistanceBetweenObjects = 1f; // Minimum distance between spawned objects

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        SpawnObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Destroy the previously spawned objects
            DestroySpawnedObjects();

            // Spawn new objects
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        if (objectPrefabs == null || objectPrefabs.Length == 0)
        {
            Debug.LogError("No object prefabs assigned!");
            return;
        }

        // Calculate the total gap to distribute among objects
        float totalGap = spawnAreaWidth - minDistanceBetweenObjects * (numberOfObjectsToSpawn - 1);

        // Calculate the average gap size
        float averageGapSize = totalGap / Mathf.Max(1, numberOfObjectsToSpawn - 1); // Ensure at least one gap if no objects are spawned

        // Calculate the initial spawn position
        Vector3 spawnPos = new Vector3(transform.position.x - spawnAreaWidth / 2f, transform.position.y, spawnZPosition);

        // Spawn objects at calculated positions
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Get the object prefab to spawn
            GameObject prefabToSpawn = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

            // Spawn the object at the calculated position
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            spawnedObject.tag = "SpawnedObject";

            // Add Rigidbody component
            Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;

            // Add the spawned object to the list
            spawnedObjects.Add(spawnedObject);

            // Update the spawn position for the next object
            spawnPos.x += minDistanceBetweenObjects + averageGapSize;
        }
    }

    void DestroySpawnedObjects()
    {
        // Destroy all spawned objects
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            Destroy(spawnedObject);
        }
        // Clear the list of spawned objects
        spawnedObjects.Clear();
    }

    void OnDrawGizmosSelected()
    {
        // Draw wireframe rectangle representing the spawn area on the X and Y axes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, 0f, spawnZPosition), new Vector3(spawnAreaWidth, spawnAreaHeight, 0f));
    }

    public void SimulateSpaceKeyPress()
    {
        // Destroy all spawned objects
        DestroySpawnedObjects();

        // Spawn new objects
        SpawnObjects();
    }
}


