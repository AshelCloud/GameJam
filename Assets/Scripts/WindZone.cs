using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float fanForce = 20f; // 선풍기가 가할 힘
    [SerializeField]
    private float fanRange = 8f; // 선풍기의 범위
    public bool windon = true;
    [SerializeField]
    private Vector2 fanDirection = Vector2.right; // 선풍기가 가리키는 방향

    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && windon)
        {
            Vector2 directionToPlayer = other.transform.position - transform.position;
            float distance = directionToPlayer.magnitude;

            if (distance <= fanRange)
            {
                // 플레이어에게 힘을 가합니다.
                other.GetComponent<Rigidbody2D>().AddForce(fanDirection * fanForce);
            }
        }
    }

    public void Disable()
    {
        windon = false;
        m_Animator.SetBool("NoWind", true);
    }

    private void OnDrawGizmosSelected()
    {
        // 범위를 시각적으로 표시합니다.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fanRange);
    }
}
