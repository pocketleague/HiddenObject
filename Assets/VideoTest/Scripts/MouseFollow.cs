using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public RectTransform followImage; // Assign the UI image RectTransform in the inspector
    public Canvas canvas;             // Assign the canvas in the inspector

    private bool isFollowing = true;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        //// Start following the cursor when the left mouse button is pressed
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isFollowing = true;
        //}

        //// Stop following the cursor when the left mouse button is released
        //if (Input.GetMouseButtonUp(0))
        //{
        //    isFollowing = false;
        //}

        // Update the position of the image to follow the cursor
        if (isFollowing)
        {
            Vector3 mousePos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out mousePos);
            followImage.position = mousePos;
        }
    }
}
