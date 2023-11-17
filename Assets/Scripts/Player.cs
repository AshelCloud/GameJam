using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_BoxCollider;
    private Animator m_Animator;

    [SerializeField]
    private float m_Speed = 5.0f;

    [SerializeField]
    private float m_JumpPower = 5.0f;

    [SerializeField]
    private float m_ClimingSpeed = 5.0f;

    private float m_DistToGround = 0.1f;

    private bool m_IsCliming = false;

    [SerializeField]
    private LayerMask m_GrondLayerMask;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider= GetComponent<BoxCollider2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if(IsGrounded())
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
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        m_Rigidbody.velocity = new Vector2(h * m_Speed, m_Rigidbody.velocity.y);

        if(h == 0f)
        {
            m_Animator.SetBool("Walk", false);
        }
        else
        {
            FlipX(h > 0f);
            m_Animator.SetBool("Walk", true);
        }
    }

    private void Climing()
    {
        if(IsGrounded())
        {
            return;
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsGrounded() == false)
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

    List<SpriteRenderer> m_SpriteRenderers;
    private void FlipX(bool flipX)
    {
        if(m_SpriteRenderers == null)
        {
            m_SpriteRenderers= new List<SpriteRenderer>();
            m_SpriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        foreach(var renderer in m_SpriteRenderers) 
        {
            renderer.flipX = flipX;
        }
    }

    private bool IsGrounded()
    {
        bool ret = Physics2D.Raycast(transform.position, -Vector3.up, m_DistToGround, m_GrondLayerMask);
        return ret;
    }
}
