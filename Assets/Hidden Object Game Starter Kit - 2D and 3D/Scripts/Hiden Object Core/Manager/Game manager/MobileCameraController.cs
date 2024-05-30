using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCameraController : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float minXLimitPercent = -0.2f; // Limit percent of starting position
    public float maxXLimitPercent = 0.2f; // Limit percent of starting position
    public float minYLimitPercent = -0.2f; // Limit percent of starting position
    public float maxYLimitPercent = 0.2f; // Limit percent of starting position
    public float minFOV = 20f; // Minimum FOV
    public float maxFOV = 100f; // Maximum FOV
    public float FOVChangeSpeed = 10f; // Speed of FOV change
    public float tiltSpeed = 5f; // Speed of camera tilt
    public float maxTiltAngle = 45f; // Maximum tilt angle

    private Vector2 touchStartPos;
    private bool isDragging = false;
    private bool isZooming = false; // Flag to indicate if the camera is currently zooming
    private Vector3 initialPosition;
    private Camera mainCamera;

    void Start()
    {
        initialPosition = transform.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Handle pinch gesture for zooming FOV
        if (Input.touchCount == 2)
        {
            isZooming = true; // Set the flag to indicate zooming
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevMagnitude - currentMagnitude;

            mainCamera.fieldOfView += deltaMagnitudeDiff * scrollSpeed * Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, minFOV, maxFOV);
        }
        else
        {
            isZooming = false; // Reset the flag when not zooming
        }

        // Handle keyboard input for changing FOV
        if (Input.GetKey(KeyCode.K))
        {
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView + FOVChangeSpeed * Time.deltaTime, minFOV, maxFOV);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - FOVChangeSpeed * Time.deltaTime, minFOV, maxFOV);
        }

        // Handle touch input for panning the camera
        if (!isZooming && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle touch phases
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record touch start position
                    touchStartPos = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        // Calculate the drag distance
                        Vector2 dragDelta = touch.position - touchStartPos;

                        // Calculate relative limitations based on initial position
                        float minXLimit = initialPosition.x + minXLimitPercent * Screen.width;
                        float maxXLimit = initialPosition.x + maxXLimitPercent * Screen.width;
                        float minYLimit = initialPosition.y + minYLimitPercent * Screen.height;
                        float maxYLimit = initialPosition.y + maxYLimitPercent * Screen.height;

                        // Update camera position based on drag distance within relative limitations
                        float newXPos = Mathf.Clamp(transform.position.x + dragDelta.x * scrollSpeed * Time.deltaTime, minXLimit, maxXLimit); // Inverted X-axis movement
                        float newYPos = Mathf.Clamp(transform.position.y - dragDelta.y * scrollSpeed * Time.deltaTime, minYLimit, maxYLimit);
                        transform.position = new Vector3(newXPos, newYPos, transform.position.z);

                        // Check if dragging with two fingers in the same direction
                        if (Input.touchCount == 2 && Mathf.Approximately(touch.deltaPosition.x, Input.GetTouch(1).deltaPosition.x) && Mathf.Approximately(touch.deltaPosition.y, Input.GetTouch(1).deltaPosition.y))
                        {
                            // Calculate the tilt angle based on the drag direction
                            float tiltAngle = Mathf.Clamp(mainCamera.transform.localEulerAngles.x + dragDelta.y * tiltSpeed * Time.deltaTime, 0f, maxTiltAngle);

                            // Apply the tilt angle to the camera
                            mainCamera.transform.localEulerAngles = new Vector3(tiltAngle, mainCamera.transform.localEulerAngles.y, mainCamera.transform.localEulerAngles.z);
                        }

                        // Update touch start position for next frame
                        touchStartPos = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
}
