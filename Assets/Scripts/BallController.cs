using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BallController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer ln = null;

    [SerializeField]
    private float ballStopSpeed = 0.03f;

    [SerializeField]
    private float ballLaunchForce = 10.0f;

    [SerializeField]
    private float gravityScale = 1.0f;

    private Rigidbody rb = null;
    private bool isInAimingMode = false;
    private bool isIdle = true;
    private bool isAiming = false;
    private bool isBallMoving = false;
    private Vector3 lastHoldPosition = Vector3.zero;
    private CinemachineBrain camCinemachineBrain = null;
    private float timeStopped;

    private void Awake()
    {
        CacheComponents();
    }
    private void CacheComponents()
    {
        rb = GetComponent<Rigidbody>();
        camCinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        isAiming = false;
        ln.enabled = false;

        DisableMouseCursor();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < ballStopSpeed)
        {
            timeStopped += Time.deltaTime;
            if(timeStopped > 0.25f)
            {
                isBallMoving = false;
                isIdle = true;
                StopBall();
            }
        }
        else if (rb.velocity.magnitude >= ballStopSpeed)
        {
            timeStopped = 0.0f;
            isBallMoving = true;
            isAiming = false;
        }
        Aim();
   
        Vector3 gravity = Physics.gravity.y * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isAiming && !isBallMoving && isInAimingMode)
        {
            Vector3 position = GetMouseClickPosition().HasValue == false ? lastHoldPosition : GetMouseClickPosition().Value;
            LaunchBall(position);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isIdle && !isBallMoving && !isAiming)
            {
                isInAimingMode = !isInAimingMode;

                if (isInAimingMode)
                {
                    camCinemachineBrain.enabled = false;
                    ActivateMouseCursor();
                }
                else
                {
                    camCinemachineBrain.enabled = true;
                    DisableMouseCursor();
                }
            }
        }
    }

    private void Aim()
    {
        if (!isIdle || !isAiming) return;
        if (!GetMouseClickPosition().HasValue) return;
        lastHoldPosition = GetMouseClickPosition().Value;

        DrawLine(GetMouseClickPosition().Value);    
    }

    private void LaunchBall(Vector3 worldPoint)
    {
        isInAimingMode = false;
        isAiming = false;
        ln.enabled = false;
        DisableMouseCursor();
        camCinemachineBrain.enabled = true;

        Vector3 horizontalPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        Vector3 dir = (horizontalPoint - transform.position).normalized;
        float strenth = Vector3.Distance(transform.position, horizontalPoint);

        strenth = Mathf.Clamp(strenth, 0, 15);
        rb.AddForce(dir * strenth * ballLaunchForce);

        isIdle = false;
    }

    private void StopBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isIdle = true;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        if (worldPoint == null) return;

        Vector3[] allPositions = { transform.position, worldPoint };

        ln.SetPositions(allPositions);
        ln.enabled = true;
    }

    private Vector3? GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);
        if (hasHit)
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

    private void OnMouseDown()
    {
        if (!isInAimingMode) return;
        if (isIdle)
        {
            isAiming = true;
        }
    }

    private void ActivateMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}