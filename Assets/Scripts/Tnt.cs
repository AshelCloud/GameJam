using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Tnt : MonoBehaviour
{
    private CursorManager m_CursorManager;

    public float explosionRadius = 5f; // ���� �ݰ� ����

    private void Start()
    {
        m_CursorManager = Camera.main.GetComponent<CursorManager>();
    }

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
            if (col.CompareTag("Enemy") || col.CompareTag("HackObject") || col.CompareTag("BombObject"))
            {
                //����Ʈ �߰��ʿ�
                Destroy(col.gameObject); // Enemy ������Ʈ ����
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ������ �ð������� ������ (Scene �信���� ����)
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnMouseOver()
    {
        m_CursorManager.SetCursorManager("Cursor_Bomb");
    }

    private void OnMouseExit()
    {
        m_CursorManager.SetCursorManager("Cursor_Default");
    }
}
