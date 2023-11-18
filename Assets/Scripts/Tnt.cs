using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Tnt : MonoBehaviour
{
    public float explosionRadius = 5f; // 폭발 반경 설정

    void Update()
    {
        // 마우스 오른쪽 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(1))
        {
            Explode();
        }
    }

    void Explode()
    {
        // TNT 주변에 있는 오브젝트를 찾음
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            // Enemy 태그를 가진 오브젝트인지 확인
            if (col.CompareTag("Enemy")|| col.CompareTag("HackObject")|| col.CompareTag("BombObject"))
            {
                //이펙트 추가필요
                Destroy(col.gameObject); // Enemy 오브젝트 삭제
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 폭발 범위를 시각적으로 보여줌 (Scene 뷰에서만 보임)
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
