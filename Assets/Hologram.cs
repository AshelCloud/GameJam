using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasCollided = false;
    public float throwAngle = 45f; // ���� ����
    public float throwForce = 10f; // ���� ��
    public float fixedHeight = 5f; // ������ ����
    public float fixedDistance = 10f; // ������ �Ÿ�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ThrowObject();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasCollided)
        {
            rb.velocity = Vector2.zero; // �ӵ��� 0���� �����Ͽ� ����
            rb.gravityScale = 0f; // �߷��� 0���� �����Ͽ� ���ϸ� ����
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // ���� ����

            hasCollided = true;
        }
    }
    void ThrowObject()
    {
        float radians = throwAngle * Mathf.Deg2Rad; // ������ �������� ��ȯ
        float x = Mathf.Cos(radians) * fixedDistance; // x ���� �Ÿ� ���
        float y = Mathf.Sin(radians) * fixedDistance; // y ���� �Ÿ� ���

        Vector2 targetPosition = new Vector2(x, fixedHeight); // ��ǥ ���� ����

        // ��ǥ ���������� �Ÿ��� ������ �� ���
        float projectileVelocity = Mathf.Sqrt(fixedDistance * Physics2D.gravity.magnitude / Mathf.Sin(2 * radians));
        Vector2 velocity = projectileVelocity * targetPosition.normalized;

        rb.velocity = velocity; // ��ü�� �� ����
    }
}
