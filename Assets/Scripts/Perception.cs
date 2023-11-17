using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    private bool isRecognize = false;
    private GameObject target;

    public bool GetPerception() { return isRecognize; }
    public GameObject GetPerceptionTarget() { return target;}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            isRecognize = true;
            target = coll.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            isRecognize = false;
            target = null;
        }
    }
}
