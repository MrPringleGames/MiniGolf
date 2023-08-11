using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    public GameObject player;
    public Transform teleportDestination;

    private bool isTeleporting = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTeleporting = true;
            TeleportPlayer(other.gameObject);
        }
    }

    void TeleportPlayer(GameObject player)
    {
        player.transform.position = teleportDestination.position;
        isTeleporting = false;
    }
}