using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    private bool isPlayerInRange = false;
    private WindZone windZoneInstance;

    private void Start()
    {
        windZoneInstance = FindObjectOfType<WindZone>();
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonDown(1)) 
        {
            windZoneInstance.windon = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange= true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange= false;
        }
    }
}
