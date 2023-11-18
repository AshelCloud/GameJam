using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasCollided = false;
    private float throwAngle = 45f; // ���� ����
    private float fixedHeight = 5f; // ������ ����
    private float fixedDistance = 10f; // ������ �Ÿ�
    private int rightThrow = 0;
    private Player player;
    [SerializeField]
    private GameObject hologram;
    private bool spawnCount = false; //�ѹ��� ��ġ�ǰ� ��

    Vector2 previousPosition; // ���� �������� ��ġ
    float timeThreshold = 0.1f; // �������� ���ٰ� �Ǵ��� �ð�
    float timer = 0.0f; // ��� �ð�

    void Start()
    {
        player = FindObjectOfType<Player>(); // Player ��ü �Ҵ�
        if (player == null)
        {
            Debug.LogError("Player object not found!");
            return;
        }
        rb = GetComponent<Rigidbody2D>();
        ThrowObject();
    }

    private void Update()
    {
        // ���� ��ġ�� ���� ��ġ�� ���Ͽ� ������ ����
        if ((Vector2)transform.position == previousPosition)
        {
            timer += Time.deltaTime; // �������� ���� ��� �ð��� ����
            if (timer >= timeThreshold)
            {
                // ���� �ð����� �������� ���� ��� ó���� �۾�
                SpawnHologram();
                spawnCount = true;
                hasCollided = true;
            }
        }
        else
        {
            // ��ġ�� ����� ��� Ÿ�̸� �ʱ�ȭ
            timer = 0.0f;
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        previousPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasCollided)
        {
            rb.velocity = Vector2.zero; // �ӵ��� 0���� �����Ͽ� ����
            rb.gravityScale = 1f; // �߷��� 1���� �����Ͽ� ����
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //ȸ�� ����
        }
        else if (collision.gameObject.CompareTag("Player"))
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
            rightThrow = 1;
        }
        else
        {
            rightThrow = -1;
        }
        return rightThrow;
    }

    void SpawnHologram()
    {
        Instantiate(hologram, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
