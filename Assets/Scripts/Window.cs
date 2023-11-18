using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_Speed = 1f;

    private void Update()
    {
        Vector3 dir = (m_Target.position - transform.position).normalized;
        dir.y = 0f;
        dir.z = 0f;

        transform.position += dir * m_Speed * Time.deltaTime;
    }
}
