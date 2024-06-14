using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongClickSpawner : MonoBehaviour
{
    public GameObject wrongImage;
    public Canvas canvas;

    public void SpawnWrongImage()
    {
        Vector3 mousePos = Input.mousePosition;

        // Instantiate the UI image prefab
        GameObject spawnedImage = Instantiate(wrongImage, canvas.transform);

        // Set the position of the spawned image to the mouse position
        RectTransform rectTransform = spawnedImage.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = mousePos - new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }
}
