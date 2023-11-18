using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Tnt : MonoBehaviour
{
    public float explosionRadius = 5f; // ���� �ݰ� ����

    void Update()
    {
        // ���콺 ������ ��ư�� ������ ��
        if (Input.GetMouseButtonDown(1))
        {
            Explode();
        }
    }

    void Explode()
    {
        // TNT �ֺ��� �ִ� ������Ʈ�� ã��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            // Enemy �±׸� ���� ������Ʈ���� Ȯ��
            if (col.CompareTag("Enemy"))
            {
                //����Ʈ �߰��ʿ�
                Destroy(col.gameObject); // Enemy ������Ʈ ����
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ������ �ð������� ������ (Scene �信���� ����)
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
