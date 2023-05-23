using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if(currentTeleporter != null) {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Teleporter")) {
            currentTeleporter = other.gameObject;
            Debug.Log("Inside a teleporter");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Teleporter")) {
            if(other.gameObject == currentTeleporter) {
                currentTeleporter = null;
                Debug.Log("Inside a teleporter");
            }
        }
    }
}
