using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float fanForce = 20f; // ��ǳ�Ⱑ ���� ��
    [SerializeField]
    private float fanRange = 8f; // ��ǳ���� ����
    public bool windon = true;
    [SerializeField]
    private Vector2 fanDirection = Vector2.right; // ��ǳ�Ⱑ ����Ű�� ����

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
                // �÷��̾�� ���� ���մϴ�.
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
        // ������ �ð������� ǥ���մϴ�.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fanRange);
    }
}
