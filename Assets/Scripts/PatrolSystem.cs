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
    public Transform[] tr = new Transform[2];

    private bool isGameStart = false;

    private void Start()
    {
        isGameStart= true;
    }

    private void OnDrawGizmos()
    {
        if (!isGameStart) Draw();
    }

    protected virtual void Draw()
    {

    }
}
