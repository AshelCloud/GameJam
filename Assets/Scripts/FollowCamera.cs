using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //what we are following
    public Transform target;

    //zeros out velocity
    Vector3 velocity = Vector3.zero;

    //time to follow target
    public float smoothTime = .15f;

    [SerializeField]
    Vector3 offset = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 targetPos = target.position;

        //align the camera and the target z position
        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime) + offset;
    }
}
