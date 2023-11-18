using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrol : PatrolSystem
{
    public PatrolDirection direction;
    public bool isPatrol = true;

    protected override void Draw()
    {
        Gizmos.color = Color.blue;

        if (direction == PatrolDirection.Horizontal)
            Gizmos.DrawLine(new Vector3(tr[0].position.x, transform.position.y, 0f), new Vector3(tr[1].position.x, transform.position.y, 0f));
        else
            Gizmos.DrawLine(new Vector3(transform.position.x, tr[0].position.y, 0f), new Vector3(transform.position.x, tr[1].position.y, 0f));
    }
}
