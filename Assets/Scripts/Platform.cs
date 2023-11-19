using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool playerCheck;
    PlatformEffector2D platformObject;

    private void Start()
    {
        playerCheck = false;
        platformObject = transform.GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && playerCheck) 
        { 
            platformObject.rotationalOffset = 180f;
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            platformObject.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        playerCheck = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCheck = false;
    }
}
