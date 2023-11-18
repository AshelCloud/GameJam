using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolDirection
{
    Horizontal,
    Vertical
}

public class PatrolSystem : MonoBehaviour
{
    public PatrolDirection direction;
    public Transform[] tr = new Transform[2];

    private bool isGameStart = false;

    private void Start()
    {
        isGameStart= true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (!isGameStart)
        {
            if (direction == PatrolDirection.Horizontal)
                Gizmos.DrawLine(new Vector3(tr[0].position.x, transform.position.y, 0f), new Vector3(tr[1].position.x, transform.position.y, 0f));
            else
                Gizmos.DrawLine(new Vector3(transform.position.x, tr[0].position.y, 0f), new Vector3(transform.position.x, tr[1].position.y, 0f));
        }
    }
}
