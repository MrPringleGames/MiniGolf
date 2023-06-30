using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForceController : MonoBehaviour
{
    public float maxPower = 10f;
    public float powerIncrement = 0.5f;
    public float forceMultiplier = 1f;

    private float currentPower = 0f;
    private Vector3 initialPosition;
    private Rigidbody rb;
    private Camera MainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCamera = Camera.main;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPower = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            if (currentPower < maxPower)
            {
                currentPower += powerIncrement * Time.deltaTime;
            }
            else
            {
                currentPower = maxPower;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ApplyForce();
        }
    }

    private void ApplyForce()
    {
        Vector3 raycastDirection = MainCamera.transform.forward;
        rb.AddForce(raycastDirection * currentPower * forceMultiplier, ForceMode.Impulse);
        currentPower = 0f;
    }

    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
    }
}

/*In this script, the `BallController` class allows you to control the golf ball's power based on how long the mouse button 1 is pressed. When the button is released, the ball receives a force in the direction of the camera's raycast. The power increment, maximum power, and force multiplier can be adjusted to suit your needs.

Make sure you attach this script to the golf ball GameObject and assign the appropriate values in the Inspector, such as the maximum power, power increment, and force multiplier.

To use this script, create a camera in your scene and ensure it has the "MainCamera" tag assigned. When you run the game and press and hold the mouse button 1, the power of the ball will increase. Releasing the button will apply the force to the ball in the direction the camera is facing.

You can also add a separate script to reset the ball's position and velocity, like a button or trigger, and call the `ResetBall()` method on the `BallController` script to reset the ball to its initial position.
*/