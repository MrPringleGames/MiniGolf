using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; //Reference to the ball's transform
    public float smoothSpeed = 0.125f; //Smoothing factor for camera movement
    public Vector3 offset; //Offset between the camera and the ball

    private Vector3 velocity = Vector3.zero; //Velocity for smooth damping


   void LateUpdate()
    {
    //Calculate the desired position for the camera

   Vector3 desiredPosition = target.position + offset;

    //Use SmoothDamp to gradually move the camera towards the desired position
   Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

    //Update the camera's position
        transform.position = smoothedPosition;

   // Rotate the camera based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
    transform.RotateAround(target.position, Vector3.up, mouseX);

   // Move the ball in the direction the camera is facing when left click is pressed
        if (Input.GetMouseButton(0))
    {
        Vector3 direction = transform.forward;
        target.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        }
    }
}
