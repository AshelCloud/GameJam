using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_BoxCollider;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private float m_Speed = 5.0f;

    [SerializeField]
    private float m_JumpPower = 5.0f;

    [SerializeField]
    private float m_ClimingSpeed = 5.0f;

    private bool m_IsGrouned = false;
    private bool m_IsCliming = false;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider= GetComponent<BoxCollider2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();

        if(m_IsGrouned)
        {
            Jump();
        }

        if(m_IsCliming)
        {
            Climing();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode2D.Impulse);
            m_IsGrouned = false;
        }
    }

    private void Move()
    {
        Vector2 move = new Vector2(0f, m_Rigidbody.velocity.y);
        if(Input.GetKey(KeyCode.A))
        {
            move.x = -m_Speed;
            m_SpriteRenderer.flipX = false;
        }
       else  if(Input.GetKey(KeyCode.D))
        {
            move.x = m_Speed;
            m_SpriteRenderer.flipX = true;
        }
        m_Rigidbody.velocity = move;
    }

    private void Climing()
    {
        Vector2 move = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move.y = m_ClimingSpeed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            move.y = -m_ClimingSpeed;
        }

        m_Rigidbody.velocity = move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Tilemap>())
        {
            m_IsGrouned = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(m_IsGrouned == false)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                m_IsCliming = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_IsCliming = false;
    }
}
