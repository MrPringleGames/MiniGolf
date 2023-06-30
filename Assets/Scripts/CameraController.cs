using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 2f;
    public bool invertYAxis = false;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;
    private Vector2 rotationInput;

    private void LateUpdate()
    {
        HandleInput();
        FollowTarget();
    }

    private void HandleInput()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSpeed;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSpeed * (invertYAxis ? -1f : 1f);
    }

    private void FollowTarget()
    {
        desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        desiredRotation = Quaternion.Euler(rotationInput.y, rotationInput.x, 0f);
        transform.rotation = desiredRotation;
    }
}

/*In this script, the `CameraController` class smoothly follows the ball (target) while allowing the player to look around. It uses the `LateUpdate` function to ensure the camera's movement occurs after the ball has been updated.

To use this script, follow these steps:

1.Attach the `CameraController` script to the camera object in your scene.
2. Assign the ball's (target) transform to the `target` variable in the Inspector.
3. Adjust the `offset` variable to position the camera at the desired distance from the ball.
4. Optionally, you can tweak the `smoothSpeed`, `rotationSpeed`, and `invertYAxis` variables to adjust the camera movement and rotation to your preference.

Now, when you run the game, the camera will smoothly follow the ball while allowing you to look around by moving the mouse. The ball will act as the anchor, and the camera's rotation will follow the mouse movement.*/