using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperPatrol : PatrolSystem
{
    public bool isRandomPatrol = false;

    protected override void Draw()
    {
        if (!isRandomPatrol)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector3(tr[0].position.x, transform.position.y, 0f), new Vector3(tr[1].position.x, transform.position.y, 0f));
        }
    }
}
