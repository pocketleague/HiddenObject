using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // List of prefabs to spawn
    public Vector3 spawnAreaCenter; // Center of the spawn area
    public Vector3 spawnAreaSize; // Size of the spawn area
    public float minDistanceBetweenObjects = 2.0f; // Minimum distance between spawned objects
    public int maxAttemptsPerObject = 10; // Maximum number of attempts to find a valid spawn position per object
    public float spawnInterval = 1f; // Time interval between spawns

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Start spawning objects
        SpawnObjects();
    }

    void Update()
    {
        // Check for SPACE key press to destroy and respawn objects
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Destroy all spawned objects
            foreach (GameObject obj in spawnedObjects)
            {
                Destroy(obj);
            }
            spawnedObjects.Clear();

            // Respawn objects
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        // List to keep track of spawned object positions
        List<Vector3> spawnedPositions = new List<Vector3>();

        foreach (GameObject prefabToSpawn in prefabsToSpawn)
        {
            // Attempt to spawn each prefab
            int attempts = 0;
            bool spawned = false;
            while (!spawned && attempts < maxAttemptsPerObject)
            {
                // Calculate random spawn position within the spawn area
                Vector3 spawnPosition = new Vector3(
                    Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                    Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                    Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
                );

                // Check for collisions with previously spawned objects
                bool collisionDetected = false;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minDistanceBetweenObjects)
                    {
                        collisionDetected = true;
                        break;
                    }
                }

                // If collision detected, try a new spawn position
                if (collisionDetected)
                {
                    attempts++;
                    continue;
                }

                // Instantiate the prefab at the spawn position
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

                // Add BoxCollider to the spawned object
                BoxCollider boxCollider = spawnedObject.AddComponent<BoxCollider>();
                // Adjust the size of the collider as needed
                boxCollider.size = spawnedObject.GetComponent<Renderer>().bounds.size;

                // Add Rigidbody component to the spawned object
                Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
                // Adjust Rigidbody settings as needed
                rb.mass = 1f;
                rb.drag = 0.5f;
                rb.angularDrag = 0.5f;
                rb.useGravity = true; // Enable gravity
                //rb.freezeRotation = true; // Freeze Rotation


                // Add spawned object to the list
                spawnedObjects.Add(spawnedObject);
                // Add spawned position to the list
                spawnedPositions.Add(spawnPosition);

                // Object successfully spawned
                spawned = true;
            }

            // If the object couldn't be spawned after maxAttemptsPerObject, log a warning
            if (!spawned)
            {
                Debug.LogWarning("Failed to spawn object: " + prefabToSpawn.name);
            }
        }
    }

    public void SimulateSpaceKeyPress()
    {
        // Destroy all spawned objects
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            Destroy(spawnedObject);
        }
        // Clear the list of spawned objects
        spawnedObjects.Clear();

        SpawnObjects();
    }
}
