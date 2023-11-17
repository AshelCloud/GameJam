using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasCollided = false;
    [SerializeField]
    private float throwAngle = 45f; // ���� ����
    [SerializeField]
    private float throwForce = 10f; // ���� ��
    [SerializeField]
    private float fixedHeight = 5f; // ������ ����
    [SerializeField]
    private float fixedDistance = 10f; // ������ �Ÿ�
    private int rightThrow = 0;
    private Player player;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
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
        else if(collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    void ThrowObject()
    {
        float radians = throwAngle * Mathf.Deg2Rad; // ������ �������� ��ȯ
        float x = Mathf.Cos(radians) * fixedDistance * Rightcheck(); // x ���� �Ÿ� ���
        float y = Mathf.Sin(radians) * fixedDistance; // y ���� �Ÿ� ���

        Vector2 targetPosition = new Vector2(x, fixedHeight); // ��ǥ ���� ����

        // ��ǥ ���������� �Ÿ��� ������ �� ���
        float projectileVelocity = Mathf.Sqrt(fixedDistance * Physics2D.gravity.magnitude / Mathf.Sin(2 * radians));
        Vector2 velocity = projectileVelocity * targetPosition.normalized;

        rb.velocity = velocity; // ��ü�� �� ����
    }

    int Rightcheck()
    {
        bool right = player.GetisRight();
        if (right)
        {
            Debug.Log("right");
            rightThrow = 1;
        }
        else
        {
            Debug.Log("left");
            rightThrow = -1;
        }
        return rightThrow;
    }
}
