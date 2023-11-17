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

    [SerializeField]
    private float m_DistToGround = 0.1f;

    private float m_DistToWall = 0.1f;

    public bool m_IsRight = false;

    [SerializeField]
    private LayerMask m_GrondLayerMask;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider= GetComponent<BoxCollider2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_DistToWall = m_BoxCollider.bounds.extents.x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            m_Animator.SetBool("SideWalk", false);
            m_Animator.SetBool("SideIdle", true);
        }
        m_Animator.SetBool("Jump", !IsGrounded());

        if(IsGrounded())
        {
            Jump();
        }

        if (CanCliming())
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
            m_IsRight = h > 0f;
            m_Animator.SetFloat("IsRight", m_IsRight ? 1f : 0f);
            m_Animator.SetBool("Walk", true);
        }
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.AddForce(Vector2.right * m_JumpPower, ForceMode2D.Impulse);
        }

        m_Animator.SetBool("SideWalk", move != Vector2.zero);

        m_Rigidbody.velocity = move;
    }

    private bool CanCliming()
    {
        Vector2 dir = m_IsRight ? Vector2.right : Vector2.left;

        bool ret = Physics2D.Raycast(transform.position, dir, m_DistToWall + 0.1f, m_GrondLayerMask);

        m_Animator.SetBool("SideIdle", ret);
        m_Animator.SetBool("SideWalk", ret);

        return ret;
    }

    private bool IsGrounded()
    {
        bool ret = Physics2D.Raycast(transform.position, -Vector3.up, m_DistToGround, m_GrondLayerMask);

        return ret;
    }
    public bool GetisRight()
    {
        return m_IsRight;
    }
}
