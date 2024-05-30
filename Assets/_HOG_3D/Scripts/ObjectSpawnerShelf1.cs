using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerShelf1 : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Array to hold the prefabs assigned in the editor
    public int numberOfObjectsToSpawn = 10; // Number of objects to spawn
    public Vector3 spawnAreaSize = new Vector3(5f, 5f, 5f); // Size of the spawn area
    public Vector3 spawnPosition = Vector3.zero; // Position for spawning objects
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
            DestroySpawnedObjects();
            SpawnObjects();
        }
    }

    void GenerateSpawnPositions()
    {
        // Create a list of potential spawn positions within the spawn area
        List<Vector3> potentialSpawnPositions = new List<Vector3>();
        float gridSize = minDistanceBetweenObjects * 2f;
        int numCellsX = Mathf.FloorToInt(spawnAreaSize.x / gridSize);
        int numCellsY = Mathf.FloorToInt(spawnAreaSize.y / gridSize);
        int numCellsZ = Mathf.FloorToInt(spawnAreaSize.z / gridSize);
        for (int x = 0; x < numCellsX; x++)
        {
            for (int y = 0; y < numCellsY; y++)
            {
                for (int z = 0; z < numCellsZ; z++)
                {
                    float posX = spawnPosition.x - spawnAreaSize.x / 2f + gridSize / 2f + x * gridSize;
                    float posY = spawnPosition.y - spawnAreaSize.y / 2f + gridSize / 2f + y * gridSize;
                    float posZ = spawnPosition.z - spawnAreaSize.z / 2f + gridSize / 2f + z * gridSize;
                    potentialSpawnPositions.Add(new Vector3(posX, posY, posZ));
                }
            }
        }

        // Shuffle the potential spawn positions to randomize placement
        ShuffleList(potentialSpawnPositions);

        // Spawn objects one at a time
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Get the next object prefab from the list
            GameObject prefabToSpawn = objectPrefabs[i % objectPrefabs.Length];

            // Find a suitable spawn position for the object
            Vector3 spawnPos = Vector3.zero;
            bool foundValidPosition = false;
            foreach (Vector3 pos in potentialSpawnPositions)
            {
                bool validPosition = true;
                foreach (GameObject obj in spawnedObjects)
                {
                    if (Vector3.Distance(pos, obj.transform.position) < minDistanceBetweenObjects)
                    {
                        validPosition = false;
                        break;
                    }
                }
                if (validPosition)
                {
                    spawnPos = pos;
                    foundValidPosition = true;
                    break;
                }
            }

            // If a valid position is found, spawn the object
            if (foundValidPosition)
            {
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                spawnedObject.AddComponent<Rigidbody>(); // Add Rigidbody component
                float randomRotationY = Random.Range(0f, 360f); // Random rotation on the Y-axis
                spawnedObject.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f); // Apply random rotation on the Y-axis
                spawnedObjects.Add(spawnedObject);
            }
            else
            {
                Debug.LogWarning("Failed to find a valid position for object " + prefabToSpawn.name);
            }
        }
    }

    void SpawnObjects()
    {
        if (objectPrefabs == null || objectPrefabs.Length == 0)
        {
            Debug.LogError("No object prefabs assigned!");
            return;
        }

        GenerateSpawnPositions();
    }

    void DestroySpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + spawnPosition, spawnAreaSize);
    }

    public void SimulateSpaceKeyPress()
    {
        DestroySpawnedObjects();
        SpawnObjects();
    }
}
