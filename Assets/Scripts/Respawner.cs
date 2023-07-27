using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private KeyCode respawnKey = KeyCode.R;

    private BallController ballController = null;
    private Vector3 ballInitialPosition = Vector3.zero;

    private void Awake()
    {
        ballController = FindObjectOfType<BallController>();
        ballInitialPosition = ballController.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(respawnKey))
        {
            ballController.transform.position = ballInitialPosition;
            ballController.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}