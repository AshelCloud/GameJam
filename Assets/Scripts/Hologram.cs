using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasCollided = false;
    private float throwAngle = 45f; // 던질 각도
    private float fixedHeight = 5f; // 고정된 높이
    private float fixedDistance = 10f; // 고정된 거리
    private int rightThrow = 0;
    private Player player;
    [SerializeField]
    private GameObject hologram;
    private bool spawnCount = false; //한번만 설치되게 함

    Vector2 previousPosition; // 이전 프레임의 위치
    float timeThreshold = 0.1f; // 움직임이 없다고 판단할 시간
    float timer = 0.0f; // 경과 시간

    void Start()
    {
        player = FindObjectOfType<Player>(); // Player 객체 할당
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
        // 현재 위치와 이전 위치를 비교하여 움직임 감지
        if ((Vector2)transform.position == previousPosition)
        {
            timer += Time.deltaTime; // 움직임이 없는 경우 시간을 누적
            if (timer >= timeThreshold)
            {
                // 일정 시간동안 움직임이 없는 경우 처리할 작업
                SpawnHologram();
                spawnCount = true;
                hasCollided = true;
            }
        }
        else
        {
            // 위치가 변경된 경우 타이머 초기화
            timer = 0.0f;
        }

        // 현재 위치를 이전 위치로 업데이트
        previousPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasCollided)
        {
            rb.velocity = Vector2.zero; // 속도를 0으로 설정하여 멈춤
            rb.gravityScale = 1f; // 중력을 1으로 설정하여 낙하
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //회전 멈춤
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    void ThrowObject()
    {
        float radians = throwAngle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        float x = Mathf.Cos(radians) * fixedDistance * Rightcheck(); // x 방향 거리 계산
        float y = Mathf.Sin(radians) * fixedDistance; // y 방향 거리 계산

        Vector2 targetPosition = new Vector2(x, fixedHeight); // 목표 지점 설정

        // 목표 지점까지의 거리와 각도로 힘 계산
        float projectileVelocity = Mathf.Sqrt(fixedDistance * Physics2D.gravity.magnitude / Mathf.Sin(2 * radians));
        Vector2 velocity = projectileVelocity * targetPosition.normalized;

        rb.velocity = velocity; // 물체에 힘 적용
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
