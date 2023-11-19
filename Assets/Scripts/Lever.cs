using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    private Animator m_Animator;

    private bool isPlayerInRange = false;

    [SerializeField]
    private WindZone windZoneInstance;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonDown(1))  //¿ìÅ¬¸¯
        {
            windZoneInstance.Disable();
            m_Animator.SetBool("Activate", true);
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
