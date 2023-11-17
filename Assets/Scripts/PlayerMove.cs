using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_BoxCollider;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider= GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        m_Rigidbody.velocity = new Vector2(h, 0f);
    }
}
