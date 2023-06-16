using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallController : MonoBehaviour
{
    public float forceMultiplier = 10f; // Multiplier for the force applied to the ball
    public float maxForce = 20f; // Maximum force that can be applied to the ball

    private Rigidbody rb; // Reference to the ball's Rigidbody component
    private bool isMoving = false; // Flag to track if the ball is currently moving

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the ball is on the ground and not moving
        if (isMoving || !IsBallOnGround())
            return;

        // Get input from the player to control the ball's movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the force based on the input
        Vector3 force = new Vector3(moveHorizontal, 0f, moveVertical) * forceMultiplier;

        // Apply the force to the ball
        rb.AddForce(force);

        // Clamp the magnitude of the force to the maximum value
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxForce);

        // Set the isMoving flag to true
        isMoving = true;
    }

    bool IsBallOnGround()
    {
        // Check if the ball is touching the ground or any other surface you consider valid
        // Modify this condition based on your specific game environment
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
///Attach the BallControl script to your ball GameObject in the scene. 
///Adjust the forceMultiplier and maxForce values to control the force applied to the ball based on your desired gameplay mechanics.

///Please note that this script assumes your ball has a Rigidbody component attached to it and 
///requires appropriate collision detection setup in your scene for the IsBallOnGround() method to work correctly. 
///Adjust the method as needed to match your specific game environment and detection requirements.

/*{
    private Rigidbody rb;
    public float forceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos - transform.position).normalized;
            rb.AddForce(direction * forceMultiplier, ForceMode.Impulse);
        }
    }
}*/
