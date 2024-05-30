using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Array to hold the prefabs assigned in the editor
    public int numberOfObjectsToSpawn = 10; // Number of objects to spawn
    public float spawnAreaWidth = 5f; // Width of the spawn area along the X axis
    public float spawnAreaHeight = 5f; // Height of the spawn area along the Y axis
    public float spawnZPosition = 0f; // Z position for spawning objects
    public float minDistanceBetweenObjects = 1f; // Minimum distance between spawned objects

    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        GenerateSpawnPositions();
        SpawnObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Destroy the previously spawned objects
            DestroySpawnedObjects();

            // Generate new spawn positions and spawn objects
            GenerateSpawnPositions();
            SpawnObjects();
        }
    }

    void GenerateSpawnPositions()
    {
        // Clear the list of spawn positions
        spawnPositions.Clear();

        // Calculate the grid size based on the minimum distance between objects
        float gridSize = minDistanceBetweenObjects * 2f;

        // Calculate the number of grid cells along the X and Y axes
        int numCellsX = Mathf.FloorToInt(spawnAreaWidth / gridSize);
        int numCellsY = Mathf.FloorToInt(spawnAreaHeight / gridSize);

        // Generate spawn positions on a grid
        for (int x = 0; x < numCellsX; x++)
        {
            for (int y = 0; y < numCellsY; y++)
            {
                float posX = transform.position.x - spawnAreaWidth / 2f + gridSize / 2f + x * gridSize;
                float posY = transform.position.y - spawnAreaHeight / 2f + gridSize / 2f + y * gridSize;
                spawnPositions.Add(new Vector3(posX, posY, spawnZPosition));
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

        // Shuffle the list of spawn positions
        ShuffleList(spawnPositions);

        // Spawn objects at shuffled spawn positions
        int numObjectsToSpawn = Mathf.Min(numberOfObjectsToSpawn, spawnPositions.Count);
        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            // Spawn the object at the spawn position
            GameObject spawnedObject = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)], spawnPositions[i], Quaternion.identity);
            // Set the tag to indicate that this is a spawned object
            spawnedObject.tag = "SpawnedObject";
            // Add the spawned object to the list
            spawnedObjects.Add(spawnedObject);
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

    void OnDrawGizmosSelected()
    {
        // Draw wireframe rectangle representing the spawn area on the X and Y axes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, 0f, spawnZPosition), new Vector3(spawnAreaWidth, spawnAreaHeight, 0f));
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

        GenerateSpawnPositions();
        SpawnObjects();
    }
}
