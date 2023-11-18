using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player go = collision.GetComponent<Player>();
        if (go)
        {
            go.Die();
        }
    }
}
