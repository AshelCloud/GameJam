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
            CheckClick();
        }
    }

    void CheckClick()
    {
        // ���콺 ��ġ���� Ray�� ���� �浹�� ������Ʈ Ȯ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        // TNT�� Ŭ������ ��
        if (hit.collider != null && hit.collider.gameObject == gameObject)
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

            if(col.CompareTag("Boss"))
            {
                col.GetComponent<Boss>().GetDamage(30f);
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
        CursorManager.Instance.SetCursorManager("Cursor_Bomb");
    }

    private void OnMouseExit()
    {
        CursorManager.Instance.SetCursorManager("Cursor_Default");
    }
}
