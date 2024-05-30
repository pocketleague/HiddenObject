using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawnerInShelf : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // List of prefabs to spawn
    public Vector3 spawnAreaCenter; // Center of the spawn area relative to the parent object
    public Vector3 spawnAreaSize; // Size of the spawn area
    public float minDistanceBetweenObjects = 2.0f; // Minimum distance between spawned objects
    public int maxAttemptsPerObject = 10; // Maximum number of attempts to find a valid spawn position per object
    public float spawnInterval = 1f; // Time interval between spawns
    public int numberOfObjectsToSpawn = 10; // Number of objects to spawn

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Start spawning objects
        SpawnObjects();
    }

    void Update()
    {
        // Check for SPACE key press to destroy and respawn objects
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Destroy all spawned objects
            foreach (GameObject obj in spawnedObjects)
            {
                Destroy(obj);
            }
            spawnedObjects.Clear();

            // Respawn objects
            //SpawnObjects();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw spawn area gizmo in the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + spawnAreaCenter, spawnAreaSize);
    }

    void SpawnObjects()
    {
        // List to keep track of spawned object positions
        List<Vector3> spawnedPositions = new List<Vector3>();

        int objectsSpawned = 0;
        while (objectsSpawned < numberOfObjectsToSpawn)
        {
            foreach (GameObject prefabToSpawn in prefabsToSpawn)
            {
                if (objectsSpawned >= numberOfObjectsToSpawn)
                {
                    break;
                }

                // Attempt to spawn each prefab
                int attempts = 0;
                bool spawned = false;
                while (!spawned && attempts < maxAttemptsPerObject)
                {
                    // Calculate random spawn position within the spawn area, relative to the parent object
                    Vector3 spawnPosition = transform.position + new Vector3(
                        Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                        Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                        Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
                    );

                    // Check for collisions with other colliders
                    Collider[] colliders = Physics.OverlapSphere(spawnPosition, minDistanceBetweenObjects);
                    if (colliders.Length > 1) // OverlapSphere always includes itself, so we need to check if there are other colliders
                    {
                        // Collision detected, try a new spawn position
                        attempts++;
                        continue;
                    }

                    // Instantiate the prefab at the spawn position
                    GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform); // Set the parent to the current object's transform

                    // Add Rigidbody component to the spawned object
                    Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
                    // Adjust Rigidbody settings as needed
                    rb.mass = 1f;
                    rb.drag = 0.5f;
                    rb.angularDrag = 0.5f;
                    rb.useGravity = true; // Enable gravity
                    rb.freezeRotation = true;

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

                objectsSpawned++;
            }
        }
    }
}

