using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallForceBar : MonoBehaviour
{
    public Rigidbody rb;
    public Slider forceSlider;
    public float maxForce = 10f;
    public float forceMultiplier = 1f;
    private bool isPressed = false;
    private float pressStartTime;
    private float pressDuration;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            pressStartTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            pressDuration = Time.time - pressStartTime;
            ApplyForce();
        }

        if (isPressed)
        {
            pressDuration = Time.time - pressStartTime;
            UpdateSlider();
        }
    }

    private void ApplyForce()
    {
        float force = Mathf.Min(pressDuration * forceMultiplier, maxForce);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void UpdateSlider()
    {
        float normalizedForce = Mathf.Min(pressDuration * forceMultiplier, maxForce) / maxForce;
        forceSlider.value = normalizedForce;
    }
}
///In this script, the BallController class handles the mouse input and applies the force to the ball's Rigidbody based on the duration of the mouse button press. 
///It also updates a slider in the player's UI to reflect the force being applied.

///Make sure you have a Slider UI element in your player's UI and assign it to the forceSlider variable in the script.
///Additionally, assign the Rigidbody component of the ball to the rb variable in the Inspector.

///Adjust the maxForce and forceMultiplier variables as needed to control the maximum force and how the duration of the press affects the applied force.

///Remember to attach this script to the ball GameObject in your scene.
