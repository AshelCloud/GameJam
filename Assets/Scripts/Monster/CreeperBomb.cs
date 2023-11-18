using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperBomb : MonsterObject
{
    SpriteRenderer myRenderer;

    [SerializeField]
    Transform[] destinations;

    [SerializeField]
    float blinkTransparency = 0.3f;

    [SerializeField]
    float blinkTime = 0.3f;

    [SerializeField]
    int blinkCount = 3;

    [SerializeField]
    float patrolSpeed = 2f;

    [SerializeField]
    private float explosionRadius = 1f;

    List<Vector3> destList = new List<Vector3>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myRenderer = GetComponent<SpriteRenderer>();

        foreach(Transform tr in destinations) 
        {
            destList.Add(transform.TransformDirection(tr.position));
        }

        StartCoroutine(Patrol());
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
            Vector3 dir = destList[i] - transform.position;
            dir.y = 0f;
            transform.Translate(dir.normalized * patrolSpeed * Time.deltaTime);
            float distance = (destList[i].x - transform.position.x) >= 0 ? destList[i].x - transform.position.x : -(destList[i].x - transform.position.x);
            if (distance < 0.3f)
            {
                
                i = ++i % destList.Count;
                if (destList[i].x - transform.position.x > 0f)
                    myRenderer.flipX = false;
                else if(destList[i].x - transform.position.x < 0f)
                    myRenderer.flipX = true;
            }
            yield return null;
        }
    }
}
