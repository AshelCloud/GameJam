using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float fanForce = 10f; // ��ǳ�Ⱑ ���� ��
    public float fanRange = 5f; // ��ǳ���� ����

    public Vector2 fanDirection = Vector2.right; // ��ǳ�Ⱑ ����Ű�� ����

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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

    private void OnDrawGizmosSelected()
    {
        // ������ �ð������� ǥ���մϴ�.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fanRange);
    }
}
