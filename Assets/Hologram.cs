using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasCollided = false;
    public float throwAngle = 45f; // 던질 각도
    public float throwForce = 10f; // 던질 힘
    public float fixedHeight = 5f; // 고정된 높이
    public float fixedDistance = 10f; // 고정된 거리

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ThrowObject();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasCollided)
        {
            rb.velocity = Vector2.zero; // 속도를 0으로 설정하여 멈춤
            rb.gravityScale = 0f; // 중력을 0으로 설정하여 낙하를 멈춤
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // 전부 멈춤

            hasCollided = true;
        }
    }
    void ThrowObject()
    {
        float radians = throwAngle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        float x = Mathf.Cos(radians) * fixedDistance; // x 방향 거리 계산
        float y = Mathf.Sin(radians) * fixedDistance; // y 방향 거리 계산

        Vector2 targetPosition = new Vector2(x, fixedHeight); // 목표 지점 설정

        // 목표 지점까지의 거리와 각도로 힘 계산
        float projectileVelocity = Mathf.Sqrt(fixedDistance * Physics2D.gravity.magnitude / Mathf.Sin(2 * radians));
        Vector2 velocity = projectileVelocity * targetPosition.normalized;

        rb.velocity = velocity; // 물체에 힘 적용
    }
}
