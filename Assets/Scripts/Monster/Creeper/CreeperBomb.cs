using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperBomb : MonsterObject
{
    [SerializeField]
    CreeperPatrol patrolSystem;

    SpriteRenderer myRenderer;

    [SerializeField] float blinkTransparency = 0.3f;
    [SerializeField] float blinkTime = 0.3f;
    [SerializeField] int blinkCount = 3;

    [SerializeField] float patrolSpeed = 2f;

    [SerializeField] private float explosionRadius = 1f;

    [SerializeField]
    private AudioClip detectClip;

    PatrolSide patrolSide;
    Vector3 m_MoveDir;

    List<Vector3> destList = new List<Vector3>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myRenderer = GetComponent<SpriteRenderer>();

        OperatePatrol();
    }

    void OperatePatrol()
    {
        Vector3[] points = new Vector3[2];
        if (patrolSystem.isRandomPatrol)
        {
            if (Mathf.Abs(patrolSystem.tr[0].transform.position.x - patrolSystem.tr[1].transform.position.x) < 10)
            {
                Debug.Log("랜덤 경로 거리 10이상으로 다시 설정 바람");
                return;
            }

            float firstRandX = 0f;
            float secondRandX = 0f;

            while (Mathf.Abs(firstRandX - secondRandX) < 10 ||
                (firstRandX >= transform.position.x && secondRandX >= transform.position.x) ||
                (firstRandX <= transform.position.x && secondRandX <= transform.position.x))
            {
                firstRandX = SetRandomPatrolPoint();
                secondRandX = SetRandomPatrolPoint();
            }
            points[0] = new Vector3(firstRandX, transform.position.y, 0f);
            points[1] = new Vector3(secondRandX, transform.position.y, 0f);
        }
        else
        {
            points[0] = patrolSystem.tr[0].position;
            points[1] = patrolSystem.tr[1].position;
        }

        foreach (Vector3 pos in points)
        {
            destList.Add(transform.TransformDirection(pos));
        }

        patrolSide = destList[0].x > transform.position.x ? PatrolSide.Right : PatrolSide.Left;

        StartCoroutine(Patrol());
    }

    float SetRandomPatrolPoint()
    {
        float rand = Random.Range(patrolSystem.tr[0].transform.position.x, patrolSystem.tr[1].transform.position.x);

        return rand;
    }

    protected override void Found()
    {
        base.Found();
        CheckRotate();
        StartCoroutine(Explode());
    }

    void CheckRotate()
    {
        if (targetObj.transform.position.x - transform.position.x > 0f)
            myRenderer.flipX = false;
        else if(targetObj.transform.position.x - transform.position.x < 0f)
            myRenderer.flipX = true;
    }

    IEnumerator Explode()
    {
        int count = 0;

        SoundManager.Instance.PlayEffectClip(detectClip);
        while (count < blinkCount)
        {
            StartCoroutine(Blink());
            yield return new WaitForSeconds(blinkTime);
            count++;
        }

        Vector3 instantiatePos = transform.position;
        instantiatePos.y -= myRenderer.size.y / 2;
        Instantiate(transform.GetComponent<ExplodableObject>().GetParticle(), instantiatePos, Quaternion.identity);
        FindPlayer();
        Destroy(gameObject);
    }

    private void FindPlayer()
    {
        // TNT 주변에 있는 오브젝트를 찾음
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            // Enemy 태그를 가진 오브젝트인지 확인
            if (col.CompareTag("Enemy") || col.CompareTag("HackObject") || col.CompareTag("BombObject"))
            {
                //이펙트 추가필요
                if (col.gameObject.GetComponent<ExplodableObject>())
                {
                    col.gameObject.GetComponent<ExplodableObject>().ExplodeObj();
                }
                transform.GetComponent<ExplodableObject>().ExplodeObj();
            }

            if (col.CompareTag("Boss"))
            {
                col.GetComponent<Boss>().GetDamage(50f);
            }

            if(col.GetComponent<Player>())
            {
                col.GetComponent<Player>().Die();
            }
        }
    }

    IEnumerator Blink()
    {
        myRenderer.color = new Color(1, 1, 1, 1 - blinkTransparency);
        yield return new WaitForSeconds(0.1f);
        myRenderer.color = Color.white;
    }

    IEnumerator Patrol()
    {
        int i = 0;
        while (!perceptRange.GetPerception())
        {
            CheckSide();

            float distance = (destList[i].x - transform.position.x) >= 0 ? destList[i].x - transform.position.x : -(destList[i].x - transform.position.x);
            if (distance < 0.3f)
            {
                i = ++i % destList.Count;
                switch (patrolSide)
                {
                    case PatrolSide.Left:
                        patrolSide = PatrolSide.Right;
                        break;
                    case PatrolSide.Right:
                        patrolSide = PatrolSide.Left;
                        break;
                }
            }

            transform.Translate(m_MoveDir * patrolSpeed * Time.deltaTime);

            yield return null;
        }
    }

    void CheckSide()
    {
        switch (patrolSide)
        {
            case PatrolSide.Right:
                m_MoveDir = Vector3.right;
                myRenderer.flipX = false;
                break;
            case PatrolSide.Left:
                m_MoveDir = -Vector3.right;
                myRenderer.flipX = true;
                break;
        }
    }
}
