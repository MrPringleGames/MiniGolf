using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public Camera mainCamera;
    public Vector3 startingPosition;
    public float forceMultiplier = 2f;
    public float cameraDistance = 10f;
    public float cameraHeight = 5f;
    public float cameraSmoothTime = 0.1f;
    public float minMouseDeltaY = 10f;

    private Vector3 lastMousePosition;
    private bool isMouseDragging = false;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 lastStoppedPosition;

    void Start()
    {
        // Hide and lock the cursor
        Cursor.visible = false;

        // Store the starting position of the ball
        startingPosition = transform.position;
        lastStoppedPosition = startingPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isMouseDragging = true;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
            Cursor.visible = false;
            lastStoppedPosition = ballRigidbody.position;
        }

        // Only move the camera if the left mouse button is up and the mouse is being dragged
        if (!Input.GetMouseButton(0) && isMouseDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;

            // Move the camera horizontally based on the x-axis mouse movement
            float horizontalDelta = mouseDelta.x * 0.1f;
            mainCamera.transform.RotateAround(ballRigidbody.position, Vector3.up, horizontalDelta);

            // Move the camera vertically based on the y-axis mouse movement
            float verticalDelta = mouseDelta.y * 0.1f;
            Vector3 newCameraPosition = mainCamera.transform.position + (Vector3.up * verticalDelta);
            if (newCameraPosition.y > ballRigidbody.position.y)
            {
                mainCamera.transform.position = newCameraPosition;
            }

            lastMousePosition = currentMousePosition;
        }

        // Only apply force to the ball if the left mouse button is down and the mouse movement is positive on the y-axis
        if (Input.GetMouseButton(0) && ballRigidbody.velocity.magnitude == 0f)
        {
            // Lock the camera in place
            isMouseDragging = false;
            Cursor.visible = false;

            Vector3 forceDirection = mainCamera.transform.forward;
            forceDirection.y = 0f;

            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;
            if (mouseDelta.y > minMouseDeltaY)
            {
                float forceMagnitude = mouseDelta.y * forceMultiplier;
                ballRigidbody.AddForce(forceDirection.normalized * forceMagnitude);
            }
            lastMousePosition = currentMousePosition;
        }

        // Check for 'r' and reset the ball to the last stopped position
        if (Input.GetKeyDown(KeyCode.R))
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ballRigidbody.position = lastStoppedPosition;
        }
    }

    void LateUpdate()
    {
        // Calculate the target camera position based on the ball's position and some camera settings
        Vector3 cameraTargetPosition = ballRigidbody.position - (transform.forward * cameraDistance) + (Vector3.up * cameraHeight);
        // Move the camera smoothly towards the target position using Vector3.SmoothDamp
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, cameraTargetPosition, ref cameraVelocity, cameraSmoothTime);
        // Set the camera to look at the ball
        mainCamera.transform.LookAt(ballRigidbody.transform);
    }
}
